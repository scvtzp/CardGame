using Generics;

namespace Manager
{
    public class CardIDManager : Singleton<GameManager>
    {
        private double _number = 0;
        
        public double GenerateCardID()
        {
            _number++;
            return _number;
        }
    }
}