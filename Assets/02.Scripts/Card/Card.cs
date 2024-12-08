using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using CardGame.Entity;
using Skill;
using Unity.VisualScripting;

namespace DefaultNamespace
{
    /// <summary>
    /// 핸드의 카드 로직(드래그해서 사용, 등)을 담당.
    /// </summary>
    public class Card : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerUpHandler
    {
        [SerializeField] SpriteRenderer spriteRendererTest;   
        [SerializeField] DeckService deckService; //테스트용으로 직접 박아줌.   
        
        public TargetType _objectType; //카드 소유자의 타입.
        
        private GameManager _gameManager;
        private Entity _target;
        
        public CardData _cardData;
        public CardView _cardView;

        private Vector3 startPosition;
        private RectTransform rectTransform;
        
        public void Init(DeckService deck, CardData data)
        {
            _gameManager = GameManager.Instance;
            rectTransform = GetComponent<RectTransform>(); 
            
            _cardData = data;
            deckService = deck;
            _cardView?.UpdateData(_cardData);
        }

        public void SetPos(Vector3 position)
        {
            rectTransform.anchoredPosition = position;
            startPosition = position;
        }
        private void ResetPos() => rectTransform.anchoredPosition = startPosition;
        
        public void OnDrag(PointerEventData eventData)
        { 
            //TEST
            if(_objectType.HasFlag(TargetType.Me))
                return;
            
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            worldPosition.z = 0f; // 2D에서 z값을 고정
            transform.position = worldPosition;
        }
        
        //todo: 다른 카드와 닿는중에 몬스터 충돌하면 Trigger 씹힘. 카드에 리지디바디 빼면 되려나
        private void OnTriggerEnter2D(Collider2D other)
        {
            var obj = other.GetComponent<Entity>(); 
            if (obj == null)
                return;

            if (_cardData.GetTarget(obj, _objectType))
            {
                _cardView.SetBackEffect(true, Color.green);
                _target = obj;
            }
            else
                _cardView.SetBackEffect(true, Color.red);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _cardView.SetBackEffect(false);
            _target = null;
        }

        //이거 드래그, 클릭 기타등등 건들기만 하면 다 적용되는데 각각 예외처리 해줘야할듯?
        public void OnPointerClick(PointerEventData eventData)
        {
            //클릭했을때 액션
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_target == null)
            {
                ResetPos();
                return;
            }

            if(_objectType.HasFlag(TargetType.Ally) && _gameManager.Turn != Turn.MyTurn)
                    return;
            if(_objectType.HasFlag(TargetType.Enemy) && _gameManager.Turn != Turn.EnemyTurn)
                    return;
            
            SoundManager.Instance.PlaySfx("UseCard");
            _gameManager.Action(this, _target);
            _cardView.SetBackEffect(false);
        }

        public void UsedCard()
        {
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
        }

        public List<ISkill> GetSkill() => _cardData.GetSkill();
    }
}