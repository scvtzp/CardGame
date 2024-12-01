using System.Collections.Generic;
using System;
using System.Linq;

namespace DefaultNamespace
{
    public static class GameUtil
    {
        public static void ShuffleCollection<T>(IEnumerable<T> collection, int? seed = null)
        {
            Random rng = seed.HasValue ? new Random(seed.Value) : new Random();
    
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
    }
}