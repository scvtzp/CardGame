using System;
using CardGame.Entity;
using DefaultNamespace;
using Manager.Generics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// 마우스 포인터 출력(월드 캔버스 기준)
    /// view만 하려고 했으나 임시적으로 싱글톤 달아서 충돌 몹까지 저장.
    /// 뷰가 하는게 거의 없이 클리커 하나 띄우는게 끝이라 저 업데이트문 하나 때문에 나누기도 애매하네....
    ///
    /// todo: 생각해보니까 나중에 클릭할때 이펙트 나오고 할거 생각하면 나누는게 맞다. 이펙트 추가할때 view단이랑 분할 필요.
    /// </summary>
    public class MousePointer : NonDontDestroySingleton<MousePointer>, IPointerClickHandler
    {
        [SerializeField] RectTransform rectTransform;

        public Entity SelectedEntity { get; private set; }
        private Card SelectedCard;
        
        private void Start()
        {
            BezierCurve.Instance.endPoint = this.transform;
        }

        private void Update()
        {
            Vector2 mousePos;
            
            //만약 world캔버스 아니라면 Camera.main을 null로 바꿔주면 됨.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, 
                Input.mousePosition, Camera.main, out mousePos);
            
            // 포인터 위치 업데이트
            rectTransform.anchoredPosition = mousePos;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ;
        }

        public void SelectCard(Card card)
        {
            SelectedCard = card;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var obj = other.GetComponent<Entity>(); 
            if (obj == null)
                return;

            SelectedEntity = obj;
            SelectedCard?.TriggerEnter(SelectedEntity);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            SelectedEntity = null;
            SelectedCard?.TriggerExit();
        }
    }
}