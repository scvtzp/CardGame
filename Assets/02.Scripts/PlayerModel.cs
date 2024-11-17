using Manager.Generics;
using R3;

namespace DefaultNamespace
{
    public class PlayerModel : NonMonoSingleton<PlayerModel>
    {
        public ReactiveProperty<int> Gold =  new(0);
    }
}