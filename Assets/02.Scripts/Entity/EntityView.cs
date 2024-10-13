using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame.Entity
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] HpBar hpBar;
        
        private Entity _entity;

        public void ChangeHp(int hp, int maxhp)
        {
            hpBar.SetHpBar(hp, maxhp);
        }
    }
}