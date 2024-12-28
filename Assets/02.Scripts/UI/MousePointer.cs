using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class MousePointer : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] RectTransform rectTransform;
        
        private void Start()
        {
            BezierCurve.Instance.endPoint = this.transform;
        }

        private void Update()
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, 
                Input.mousePosition, null, out mousePos);

            // 포인터 위치 업데이트
            rectTransform.anchoredPosition = mousePos;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ;
        }
    }
}