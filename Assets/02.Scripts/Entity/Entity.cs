using DefaultNamespace;
using Manager;
using UnityEngine;

namespace CardGame.Entity
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private EntityView entityView;
        
        public ObjectType type;
        public int hp = 100;
        public int maxhp = 100;
        public DeckService deck;

        private GameManager _gameManager;
        
        private void Start()
        {
            _gameManager = GameManager.Instance;
            boxCollider.size = spriteRenderer.bounds.size;
            boxCollider.offset = spriteRenderer.bounds.center - transform.position;

            entityView?.ChangeHp(hp, maxhp);
            
            if (deck != null)
                _gameManager.AddDeck(deck);
        }
        
        
        //todo: 여기 rx구독으로 변경
        public void ChangeHp(int amount)
        {
            hp += amount;
            Debug.Log($"현재 hp:{hp}");
            
            entityView?.ChangeHp(hp, maxhp);
            
            if(hp <= 0)
                _gameManager.KillAction(this);
        }

        public void Kill()
        {
            Destroy(this.gameObject);
        }
    }
}