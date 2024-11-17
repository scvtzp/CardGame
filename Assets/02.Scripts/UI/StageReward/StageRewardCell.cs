using TMPro;
using UI.UIBase;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.StageReward
{
    public class ItemData
    {
        public string Id;
        public int Count;

        public ItemData(string id, int count)
        {
            Id = id;
            Count = count;
        }
    }
    
    public class StageRewardCell : ButtonCell
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
        
        public void UpdateData(ItemData data)
        {
            nameText.text = data.Id;
            priceText.text = data.Count.ToString();
        }
    }
}