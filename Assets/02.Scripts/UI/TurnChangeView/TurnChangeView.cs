using System.Threading.Tasks;
using DefaultNamespace;
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

        public override Task ShowStart()
        {
            titleText.SetTermAndRefresh("Turn Change");
            turnText.SetSmartString("TurnCount","Count", GameManager.Instance.turnCount.ToString());
            
            return base.ShowStart();
        }
    }
}