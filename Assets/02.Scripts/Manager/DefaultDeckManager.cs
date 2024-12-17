using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Manager;
using Manager.Generics;
using Skill;

namespace DefaultNamespace
{
    public class DefaultDeckManager : Singleton<DefaultDeckManager>
    {
        public Dictionary<string, List<CardData>> defaultDeckSetting = new();
        
        private Dictionary<string, Type> typeCache = new();
        private Dictionary<Tuple<string, Type[]>, ConstructorInfo> constructorCache = new();

        public Dictionary<string, List<ISkill>> cardBody = new();
        public Dictionary<string, CostAndTarget> cardCost = new();
        
        public void Init()
        {
            LoadCardCost();
            LoadCardBody();
            LoadEntityDefaultDeck();
        }

        private void LoadCardCost()
        {
            TextAsset csvFile = Resources.Load<TextAsset>("CostSetting");
            string[] lines = csvFile.text.Split('\n');
            
            for (var index = 1; index < lines.Length; index++) //인덱스 0은 맨 윗줄. (id, 닉네임, 클래스 써있는곳)
            {
                string[] columns = lines[index].Split(',');
                TargetType target = TargetType.None;
                foreach (var str in columns[2].Split('/'))
                    target |= Enum.Parse<TargetType>(str);
                
                cardCost.Add(columns[0], new CostAndTarget(int.Parse(columns[1]), target));
            }
        }

        /// <summary>
        /// 파일 형식 
        /// 1. 클래스는 /로 구분. 
        /// 2. 매개변수들은 클래스명 뒤에 (속에 ,로 구분해서 넣음
        /// 3. 고정 타겟은 클래스명 앞에 ^로 구분하여 넣음. (속에 ,로 구분해서 넣음
        ///
        /// 최종 예시 : 타겟^클래스(매개변수1,매개변수2/클래스(매개변수1
        /// </summary>
        private void LoadCardBody()
        {
            TextAsset csvFile = Resources.Load<TextAsset>("CardSetting");
            string[] lines = csvFile.text.Split('\n');
            
            for (var index = 1; index < lines.Length; index++) //인덱스 0은 맨 윗줄. (id, 닉네임, 클래스 써있는곳)
            {
                string[] columns = lines[index].Split(',');
                List<ISkill> action = new List<ISkill>();
                foreach (var str in columns[2].Split('/'))
                {
                    string className = str;
                    TargetType target = TargetType.None;
                    int[] param = null;
                    
                    //트리거형 스킬 따로 예외처리.
                    if (str.Contains('{')) 
                    {
                        className = str.Split('{')[0];
                        var strings = str.Split('{')[1].Split('#');
                        List<ISkill> skills = new List<ISkill>();
                        
                        #region 내부적으로 또 스킬 처리. 함수로 깔끔하게 정리 필요.
                        
                        TargetType actiontarget = TargetType.None;
                        var actionString = strings[4];
                        string actionName = actionString;
                        int[] actionParam = null;
                        
                        if (actionString.Contains('^'))
                        {
                            foreach (var v in actionString.Split('^')[0].Split(','))
                                actiontarget |= Enum.Parse<TargetType>(v);
                        
                            actionName = actionName.Split('^')[1];
                        }
                        if (actionString.Contains('('))
                        {
                            var paramString = actionString.Split('(')[1].Split(',');
                            actionParam = new int[paramString.Length];
                            for (var i = 0; i < paramString.Length; i++)
                            {
                                var s = paramString[i];
                                actionParam[i] = int.Parse(s);
                            }
                        
                            actionName = actionName.Split('(')[0];
                        }
                    
                        //생성
                        if (actiontarget != TargetType.None)
                        {
                            if(actionParam != null)
                                skills.Add((ISkill)InstantiateClassByName(actionName, actiontarget, actionParam));
                            else
                                skills.Add((ISkill)InstantiateClassByName(actionName, actiontarget));
                        }
                        else
                        {
                            if(actionParam != null)
                                skills.Add((ISkill)InstantiateClassByName(actionName, actionParam));
                            else
                                skills.Add((ISkill)InstantiateClassByName(actionName));
                        }
                        #endregion
                        
                        param = new int[1];
                        param[0] = int.Parse(strings[3]);
                        
                        action.Add((ISkill)InstantiateClassByName(className, 
                            GameUtil.StringToEnum<TriggerType>(strings[0]), 
                            GameUtil.StringToEnum<TargetType>(strings[1]), 
                            GameUtil.StringToEnum<TargetType>(strings[2]), 
                            skills, param));
                        continue;
                    }
                    
                    //일반적인 형식.
                    if (str.Contains('^'))
                    {
                        foreach (var v in str.Split('^')[0].Split(','))
                            target |= Enum.Parse<TargetType>(v);
                        
                        className = className.Split('^')[1];
                    }
                    if (str.Contains('('))
                    {
                        var paramString = str.Split('(')[1].Split(',');
                        param = new int[paramString.Length];
                        for (var i = 0; i < paramString.Length; i++)
                        {
                            var s = paramString[i];
                            param[i] = int.Parse(s);
                        }
                        
                        className = className.Split('(')[0];
                    }
                    
                    //생성
                    if (target != TargetType.None)
                    {
                        if(param != null)
                            action.Add((ISkill)InstantiateClassByName(className, target, param));
                        else
                            action.Add((ISkill)InstantiateClassByName(className, target));
                    }
                    else
                    {
                        if(param != null)
                            action.Add((ISkill)InstantiateClassByName(className, param));
                        else
                            action.Add((ISkill)InstantiateClassByName(className));
                    }
                }

                cardBody.Add(columns[0], action);
            }
        }

        private void LoadEntityDefaultDeck()
        {
            //데이터 형식: id, 코스트, 타겟, 스킬, 부가효과
            TextAsset csvFile = Resources.Load<TextAsset>("DefaultDeckSetting");
            string[] lines = csvFile.text.Split('\n');

            string id = "";
            for (var i = 1; i < lines.Length; i++) //0번은 헤더라서 뺌.
            {
                var line = lines[i];
                string[] columns = line.Split(',');

                // 첫칸에 값이 있음 = 카드 Id라는 뜻.
                // 이 이후로 다음 id가 등장하기 전까지의 모든 줄이 각각 하나의 카드.
                if (columns[0] != "")
                {
                    id = columns[0];
                    defaultDeckSetting.Add(id, new List<CardData>());
                }
                else
                {
                    int cost = int.Parse(columns[1]);

                    TargetType target = TargetType.None;
                    foreach (var type in columns[2].Split('/'))
                        target |= Enum.Parse<TargetType>(type);

                    List<ISkill> skills = cardBody[columns[3]];

                    CardData cardData = new CardData(cost, target, skills, columns[3]);
                    defaultDeckSetting[id].Add(cardData);
                }
            }
        }
        
        private object InstantiateClassByName(string className, params object[] parameters)
        {
            if (!typeCache.TryGetValue(className, out Type type))
            {
                type = Type.GetType($"Skill.{className}"); //혹시 여기(Skill.) 네임스페이스 바뀌면 바꿔줘야함.
                typeCache[className] = type;
            }

            Type[] parameterTypes = parameters.Select(p => p.GetType()).ToArray();
            var cacheKey = new Tuple<string, Type[]>(className, parameterTypes);

            if (!constructorCache.TryGetValue(cacheKey, out ConstructorInfo constructor))
            {
                constructor = type.GetConstructor(parameterTypes);
                constructorCache[cacheKey] = constructor;
            }

            return constructor.Invoke(parameters);
        }
    }
}