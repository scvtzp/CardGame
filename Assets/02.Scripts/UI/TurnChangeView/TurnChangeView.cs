using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DG.Tweening;
using I2.Loc;
using Manager;
using TMPro;
using UI.UIBase;
using UnityEngine;

namespace UI.TurnChangeView
{
    public class TurnChangeView : ViewBase
    {
        [SerializeField] private Localize titleText;
        [SerializeField] private TextMeshProUGUI turnText;

        private RectTransform _rectTransform;
        
        public override void Init()
        {
            _rectTransform = GetComponent<RectTransform>(); 
        }

        public override async UniTask ShowStart()
        {
            TextSetting();
            return;
        }

        public override async UniTask ShowEnd()
        {
            await StartAnimation();
        }

        public override void HideEnd()
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, 0);
        }

        private async UniTask StartAnimation()
        {
            await _rectTransform.DOAnchorPosY(-100, 1.3f).SetEase(Ease.OutBounce).ToUniTask();
            
            OnPressedBackKey();
            return;
        }

        private void TextSetting()
        {
            switch (GameManager.Instance.Turn)
            {
                case Turn.MyTurn:
                    titleText.SetTermAndRefresh("PlayerTurn");
                    break;
                case Turn.EnemyTurn:
                    titleText.SetTermAndRefresh("OpponentTurn");
                    break;
            }
            
            turnText.SetSmartString("TurnCount","Count", GameManager.Instance.turnCount.ToString());
        }
    }
}