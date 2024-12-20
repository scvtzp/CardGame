using UnityEngine;

namespace Manager.Generics
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    instance = gameObject.AddComponent<T>();
                }

                return instance;
            }
        }

        public virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);

            //as연산 : E as T -> E는 값을 반환하는 식이고, T는 형식 또는 형식 매개 변수의 이름입니다.
            if (instance == null)
                instance = this as T;
            else
            {
                if (instance != this)
                    Destroy(gameObject);
            }
        }
    }
}