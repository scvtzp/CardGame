namespace Generics
{
    public class NonMonoSingleton<T> where T : class, new()
    {
        private static T _instance;

        // 싱글톤 인스턴스에 접근하기 위한 프로퍼티
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        protected NonMonoSingleton()
        {
        }
    }
}