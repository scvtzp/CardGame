using System.Collections.Generic;
using DefaultNamespace;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("deck")] public DeckService deckService;

        private GameManager _gameManager;
        
        private void Start()
        {
            //todo: 스포너에서 생성될때 게임매니저에 team1/2Entity에 추가시켜줘야함.
            _gameManager = GameManager.Instance;
            boxCollider.size = spriteRenderer.bounds.size;
            boxCollider.offset = spriteRenderer.bounds.center - transform.position;

            entityView?.ChangeHp(hp, maxhp);
            
            if (deckService != null)
                _gameManager.AddDeck(deckService);
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

        public void AutoTurn()
        {
            //todo: 덱 서비스를 가지고 자동 전투 구현.
            deckService.StartDraw();
        }

        public void SetDeck(List<CardData> deck)
        {
            deckService.SetDeck(deck);
        }
    }
}