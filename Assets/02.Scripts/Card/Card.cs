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

    public class CostAndTarget
    {
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

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _gameManager.AddSkillDelegate(SkillDelegateType.Start, () => {Debug.Log("스킬 사용 전");});
            _gameManager.AddSkillDelegate(SkillDelegateType.Start, () => {Debug.Log("스킬 사용 전");});

            id = CardIDManager.Instance.GetInstanceID();
            
            deckService.AddCard(this);
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
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var obj = other.GetComponent<Entity>(); 
            if (obj == null)
                return;
            _target = obj;
            
            if (_costAndTarget.GetTarget(_target, _objectType))
            {
                spriteRendererTest.color = Color.red;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _target = null;
            spriteRendererTest.color = Color.white;
        }

        //이거 드래그, 클릭 기타등등 건들기만 하면 다 적용되는데 각각 예외처리 해줘야할듯?
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("클릭");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(_target == null)
                return;
            
            _gameManager.Action(this, _target);
        }

        public void UsedCard()
        {
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        public List<ISkill> GetSkill() => _skill;
    }
}