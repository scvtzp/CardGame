using System.Collections.Generic;
using System.Threading.Tasks;
using _02.Scripts.Manager;
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
        [SerializeField] TextRendererParticleSystem splashParticles;
        
        public TargetType type;
        public int hp = 10;
        public int maxhp = 10;
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
            if(maxhp < hp + amount)
                amount = maxhp - hp;
            hp += amount;
            Debug.Log($"{gameObject.name} 현재 hp:{hp}");
            
            entityView?.ChangeHp(hp, maxhp);
            splashParticles.SpawnParticle(transform.position, $"{amount}", amount > 0? Color.green: Color.red);
            
            if(hp <= 0)
                _gameManager.KillAction(this);
            
            //트리거 처리
            if(amount < 0)
                TriggerManager.Instance.OnTrigger(TriggerType.GetDamage, this);
            else if(amount > 0)
                TriggerManager.Instance.OnTrigger(TriggerType.GetHeal, this);
        }

        public void Kill()
        {
            Destroy(this.gameObject);
        }

        ///자동 턴(내가 아닌 적이나 생성체들 AI용)
        public async Task AutoTurn()
        {
            //todo: 덱 서비스를 가지고 자동 전투 구현.
            StartTurn();

            var handCopy = new List<CardData>(deckService.Hand);
            foreach (var cardData in handCopy)
            {
                await Task.Delay(500);
                _gameManager.Action(cardData);
            }
        }

        public void StartTurn()
        {
            deckService.StartDraw();
        }

        public void SetDeck(List<CardData> deck)
        {
            deckService.SetDeck(deck);
        }

        public void Draw(int count) => deckService.Draw(count);
    }
}