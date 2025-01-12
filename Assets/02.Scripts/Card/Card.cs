using System.Collections.Generic;
using AddSkill;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using CardGame.Entity;
using DG.Tweening;
using Skill;
using UI;

namespace DefaultNamespace
{
    /// <summary>
    /// 핸드의 카드 로직(드래그해서 사용, 등)을 담당.
    /// </summary>
    public class Card : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerUpHandler
    {
        private const float CardActiveY = 375f;
        
        [SerializeField] private SpriteRenderer spriteRendererTest;   
        [SerializeField] private DeckService deckService; //테스트용으로 직접 박아줌.   
        
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

        private void SelectEnd()
        {
            rectTransform.DOKill();
            rectTransform.DOAnchorPosY(startPosition.y, 0.5f);
            
            _cardView.SetBackEffect(false);
            
            BezierCurve.Instance.startPoint = null;
            MousePointer.Instance.SelectCard(null);
        }
        
        /// <summary>
        /// 스크롤 시작
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        { 
            rectTransform.DOKill();
            rectTransform.DOAnchorPosY(CardActiveY, 0.5f);
            
            BezierCurve.Instance.startPoint = this.transform;
            MousePointer.Instance.SelectCard(this);
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
                SelectEnd();
                return;
            }
            if(_objectType.HasFlag(TargetType.Ally) && _gameManager.Turn != Turn.MyTurn)
            {
                SelectEnd();
                return;
            }
            if(_objectType.HasFlag(TargetType.Enemy) && _gameManager.Turn != Turn.EnemyTurn)
            {
                SelectEnd();
                return;
            }
            
            SoundManager.Instance.PlaySfx("UseCard");
            _gameManager.Action(this, _target);
            SelectEnd();
        }

        public void TriggerEnter(Entity entity)
        {
            if (_cardData.GetTarget(entity, _objectType))
            {
                _cardView.SetBackEffect(true, Color.green);
                _target = entity;
            }
            else
            {
                _cardView.SetBackEffect(true, Color.red);
                _target = null;
            }
        }
        
        public void TriggerExit()
        {
            _cardView.SetBackEffect(false);
            _target = null;
        }
        
        public void UsedCard()
        {
            Destroy(this.gameObject);
        }

        public List<ISkill> GetSkill() => _cardData.GetSkill();
        public IAddSkill GetAddSkill() => _cardData.GetAddSkill();
    }
}