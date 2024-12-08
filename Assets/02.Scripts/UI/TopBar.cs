using System;
using DefaultNamespace;
using I2.Loc;
using Manager;
using R3;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TopBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stage;
        [SerializeField] private TextMeshProUGUI gold;
        
        private void Start()
        {
            StageManager.Instance.Stage.Subscribe(x => stage.text = $"{LocalizationManager.GetTranslation("stage")}: {x}");
            PlayerModel.Instance.Gold.Subscribe(x => gold.text = $"{x} {LocalizationManager.GetTranslation("gold")}");
        }
    }
}