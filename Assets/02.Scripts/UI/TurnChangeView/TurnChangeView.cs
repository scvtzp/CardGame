using System.Threading.Tasks;
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

        public override Task ShowStart()
        {
            //titleText.SetTermAndRefresh("Turn Change");
            turnText.SetSmartString("TurnCount","Count", GameManager.Instance.turnCount.ToString());
            
            return base.ShowStart();
        }

        public override void ShowEnd()
        {
            StartAnimation();
        }

        private async void StartAnimation()
        {
            _rectTransform.DOAnchorPosY(-100, 1.3f).SetEase(Ease.OutBounce).OnComplete(OnPressedBackKey);
        }
    }
}