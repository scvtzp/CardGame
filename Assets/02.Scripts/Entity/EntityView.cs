using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame.Entity
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] HpBar hpBar;
        
        private Entity _entity;

        public void ChangeHp(int hp, int maxHp)
        {
            hpBar.SetHpBar(hp, maxHp);
        }
    }
}