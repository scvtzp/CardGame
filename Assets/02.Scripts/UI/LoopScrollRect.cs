using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class LoopScrollRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
    {
        // 1. 클릭하면 스크롤
        // 2. 각 칸이 내 범위(transform)에서 완전히 벗어나면 맨 아래로 이동시킴. (아래에서 위 마찬가지)
        // 2-1. 완전히가 아니라 한칸 추가로 벗어나면 이동. ㅁㅁ|ㅁㅁㅁㅁ| ||가 범위라고 생각할때 맨 왼쪽만 아래로 이동. 왼쪽 두번째는 이동x 
        
        // 상속 후보 :
        // UIBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutGroup
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnScroll(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}