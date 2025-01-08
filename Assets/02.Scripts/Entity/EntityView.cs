using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace CardGame.Entity
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] HpBar hpBar;
        
        private Entity _entity;

        public void Init(int maxHp)
        {
            // 처음 세팅이니까 무조건 풀피.
            hpBar.SetHpBar(maxHp, maxHp, false);
        }
        
        public async UniTask ChangeHp(int hp, int maxHp)
        {
            await hpBar.SetHpBar(hp, maxHp);
        }
    }
}