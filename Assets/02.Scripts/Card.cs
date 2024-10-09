using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public interface ISkill
    {
        
    }
    
    public class Damage : ISkill
    {
        
    }

    public class CostAndTarget
    {
        public bool GetTarget(Collider2D other, ObjectType type)
        {
            if (other.GetComponent<Object>().type != type)
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
        private CostAndTarget _costAndTarget = new CostAndTarget();
        private ISkill _skill;
        //부가효과 도대체 어케함?
        
        public ObjectType _objectType; //카드 소유자의 타입.

        [SerializeField] SpriteRenderer spriteRendererTest;   
        
        float distance = 10.0f;
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
            if (_costAndTarget.GetTarget(other, _objectType))
            {
                spriteRendererTest.color = Color.red;
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            spriteRendererTest.color = Color.white;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ;
        }
    }
}