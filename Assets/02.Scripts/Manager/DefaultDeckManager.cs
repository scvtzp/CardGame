using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace DefaultNamespace
{
    // 파일 이름 디폴트 덱 매니저로 하고 여기서 json으로 카드 변환. 추가까지 다 할까?
    public class DefaultDeckManager : Singleton<DefaultDeckManager>
    {
        public Dictionary<string, List<CardData>> defaultDeckSetting = new Dictionary<string, List<CardData>>();
        
        private Dictionary<string, Type> typeCache = new Dictionary<string, Type>();
        private Dictionary<Tuple<string, Type[]>, ConstructorInfo> constructorCache = new Dictionary<Tuple<string, Type[]>, ConstructorInfo>();
        
        public void Start()
        {
            //데이터 형식: id, 코스트, 타겟, 스킬, 부가효과
            TextAsset csvFile = Resources.Load<TextAsset>("DefaultDeckSetting");
            string[] lines = csvFile.text.Split('\n');

            string id = "";
            foreach (string line in lines)
            {
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

                    List<ISkill> skills = new List<ISkill>();
                    foreach (var skill in columns[3].Split('/'))
                        skills.Add((ISkill)InstantiateClassByName(skill));
                    
                    CardData cardData = new CardData(cost, target, skills);
                    defaultDeckSetting[id].Add(cardData);
                }
            }
        }

        private object InstantiateClassByName(string className, params object[] parameters)
        {
            if (!typeCache.TryGetValue(className, out Type type))
            {
                type = Type.GetType($"DefaultNamespace.{className}");
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