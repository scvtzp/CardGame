using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using CardGame.Entity;

namespace DefaultNamespace
{
    public interface ISkill
    {
        public void StartSkill(Entity target);
    }
    
    public class Damage : ISkill
    {
        public void StartSkill(Entity target)
        {
            target.ChangeHp(-10);
        }
    }
    
    public class Heal : ISkill
    {
        public void StartSkill(Entity target)
        {
            target.ChangeHp(10);
        }
    }
    
    [Flags]
    public enum TargetType
    {
        Me = 1<<0, //본인
        Ally = 1<<1, //아군
        Enemy = 1<<2, //적군
        
        Summoner = 1<<3, //소환된 잡몹
    }
    public class CostAndTarget
    {
        //todo: 지금은 적을 타게하기로 되어있는데, 다양한 타겟을 경우에 따라 조준할 수 있도록 수정 필요.
        private TargetType targetType;
        
        public bool GetTarget(Entity other, ObjectType type)
        {
            if (other.type != type)
            {
                return true;
            }
            
            return true;
        }
    }

    public enum ObjectType //태그다는거 해서 구분해야할듯? 아니면 팀/포지션 이넘 두개로 따로 관리하던가.
    {
        Team1, //왼쪽팀(플레이어)
        Team1Create, //왼쪽팀의 소환물들
        Team2,
        Team2Create,
    }
    
    public class Card : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerUpHandler
    {
        [SerializeField] SpriteRenderer spriteRendererTest;   
        [SerializeField] DeckService deckService; //테스트용으로 직접 박아줌.   
        
        public double id; //순차적으로 1씩 늘어나는 고유 id
        public ObjectType _objectType; //카드 소유자의 타입.
        
        private GameManager _gameManager;
        private Entity _target;
        private CostAndTarget _costAndTarget = new CostAndTarget();
        private List<ISkill> _skill = new List<ISkill>()
        {
            new Damage(),
            new Damage(),
        };
        //todo: 부가효과 도대체 어케함?

        public void Init()
        {
            _gameManager = GameManager.Instance;
            // _gameManager.AddSkillDelegate(SkillDelegateType.Start, () => {Debug.Log("스킬 사용 전");});

            id = CardIDManager.Instance.GetInstanceID();
            deckService.AddCard(this);
        }

        public void Init(DeckService deck)
        {
            deckService = deck;
            Init();
        }

        public void OnDrag(PointerEventData eventData)
        { 
            //TEST
            if(_objectType != ObjectType.Team1)
                return;
            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            worldPosition.z = 0f; // 2D에서 z값을 고정
            transform.position = worldPosition;
        }
        
        //todo: 다른 카드와 닿는중에 몬스터 충돌하면 Trigger 씹힘. 카드에 리지디바디 빼면 됐었나?
        private void OnTriggerEnter2D(Collider2D other)
        {
            var obj = other.GetComponent<Entity>(); 
            if (obj == null)
                return;
            _target = obj;
            
            if (_costAndTarget.GetTarget(_target, _objectType))
            {
                if(spriteRendererTest != null)
                    spriteRendererTest.color = Color.red;
            }
            else
                Debug.Log("충돌시작" + other.name);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _target = null;
            if(spriteRendererTest != null)
                spriteRendererTest.color = Color.white;
        }

        //이거 드래그, 클릭 기타등등 건들기만 하면 다 적용되는데 각각 예외처리 해줘야할듯?
        public void OnPointerClick(PointerEventData eventData)
        {
            //클릭했을때 액션
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(_target == null)
                return;

            // 내 차례가 아니면 리턴. (사실상 플레이어 용이긴 한데... Team2껀 그냥 지울까?)
            switch (_objectType)
            {
                case ObjectType.Team1:
                case ObjectType.Team1Create:
                    if(_gameManager.Turn != Turn.MyTurn)
                        return;
                    break;
                case ObjectType.Team2:
                case ObjectType.Team2Create:
                    if(_gameManager.Turn != Turn.EnemyTurn)
                        return;
                    break;
            }
            
            _gameManager.Action(this, _target);
        }

        public void UsedCard()
        {
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        public List<ISkill> GetSkill() => _skill;
    }
}