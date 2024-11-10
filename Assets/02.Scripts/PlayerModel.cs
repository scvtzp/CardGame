using R3;
using Generics;

namespace DefaultNamespace
{
    public class PlayerModel : NonMonoSingleton<PlayerModel>
    {
        public ReactiveProperty<int> Gold =  new(0);
    }
}