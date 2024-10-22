using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(RectTransform))]
    public class UIButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler, IPointerExitHandler
    {
        [Header("기본값은 본인. 다른 요소를 타겟하려면 직접 추가해주세요. (보통 자식중 일부)")]
        [SerializeField] RectTransform animationObject;
        
        [SerializeField] private float animationTime = 0.2f;
        [SerializeField] private float pressingScale = 0.9f;
        
        //[SerializeField] private SoundType soundType = SoundType.Type1;
        
        private Button _button;
        private bool _isPressed;

        private void Start()
        {
            if (_button == null) //ReferenceEquals(_button, null) 
                TryGetComponent<Button>(out _button);
            
            if (animationObject == null)
                animationObject = (RectTransform)gameObject.transform;

            // 본인이 아닌 다른 요소 타겟시, 충돌판정용 투명 이미지 추가.
            if (animationObject != transform && gameObject.GetComponent<Graphic>() == null)
                gameObject.AddComponent<Image>().color = Color.clear;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GetComponent<Button>().interactable == false)
                return;
            
            _isPressed = true;

            PlayButtonAni(true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isPressed == false)
                return;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;

            PlayButtonAni(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isPressed == false)
                return;
        }

        private void PlayButtonAni(bool isDown)
        {
            animationObject.DOComplete();
            
            if(!_button.interactable) 
                return;

            if (isDown)
                animationObject.DOScale(pressingScale, animationTime);
            else
                animationObject.DOScale(1f, animationTime);
        }
    }
}