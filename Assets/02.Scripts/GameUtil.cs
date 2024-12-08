using System.Collections.Generic;
using System;
using System.Linq;
using I2.Loc;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public static class GameUtil
    {
        public static void ShuffleCollection<T>(IEnumerable<T> collection, int? seed = null)
        {
            System.Random rng = seed.HasValue ? new System.Random(seed.Value) : new System.Random();
    
            // 컬렉션을 리스트로 변환
            List<T> list = collection.ToList();
    
            // 리스트 섞기
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            // 컬렉션이 LinkedList일 경우 다시 업데이트
            if (collection is LinkedList<T> linkedList)
            {
                linkedList.Clear();
                foreach (var item in list)
                {
                    linkedList.AddLast(item);
                }
            }
            // List나 다른 컬렉션일 경우 리스트를 업데이트
            else if (collection is List<T> listCollection)
            {
                listCollection.Clear();
                foreach (var item in list)
                {
                    listCollection.Add(item);
                }
            }
        }

        public static void SafeAdd<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key, T2 value)
        {
            if(dictionary.ContainsKey(key))
                dictionary[key] = value;
                
            dictionary.Add(key, value);
        }

        public static T StringToEnum<T>(string str) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static void SetTermAndRefresh(this Localize localize, string key)
        {
            localize.SetTerm(key);
            
            // 번역 값이 없으면 빈 문자열로 설정.
            // 여기서 한번 가져와서 검사하고 없으면 적용하는 구조가 참 이상하긴 한데 일단 냅둠.
            // 이럴거면 굳이 localize 컴포넌트 사용 안하고 그냥 tmp.text에다가 직접 넣어줘도 무방.   
            if (string.IsNullOrEmpty(LocalizationManager.GetTranslation(key)))
                localize.SetTerm("empty");
            
            localize.OnLocalize(); // 즉시 업데이트
        }
        
        /// <summary>
        /// 랜덤 요소 1개 반환
        /// </summary>
        public static T GetRandomElement<T>(this List<T> list) 
        {
            if (list.Count == 0)
            {
                Debug.LogWarning("The list is empty.");
                return default;
            }
            int randomIndex = UnityEngine.Random.Range(0, list.Count); // 0부터 list.Count - 1까지의 랜덤 인덱스 생성
            return list[randomIndex];
        }
    }
}