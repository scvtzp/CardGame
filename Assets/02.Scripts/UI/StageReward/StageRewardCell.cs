using DefaultNamespace;
using I2.Loc;
using TMPro;
using UI.UIBase;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.StageReward
{
    public class ItemData
    {
        public readonly string Id;
        public readonly int Count;

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
        [SerializeField] private CardView cardCostCell;
        [SerializeField] private CardView cardBodyCell;
        [SerializeField] private CardView cardAddSkillCell;
        
        public void UpdateData(ItemData data)
        {
            itemIcon.gameObject.SetActive(false);
            nameText.gameObject.SetActive(false);
            cardCostCell.gameObject.SetActive(false);
            cardBodyCell.gameObject.SetActive(false);
            
            if (data.Id.Contains("part_cost"))
            {
                cardCostCell.gameObject.SetActive(true);
                cardCostCell.UpdateData(DefaultDeckManager.Instance.cardCost[data.Id.Split("part_")[1]]);
            }
            else if (data.Id.Contains("part_card"))
            {
                cardBodyCell.gameObject.SetActive(true);
                cardBodyCell.UpdateData(data.Id.Split("part_")[1]);
            }
            else if (data.Id.Contains("part_AddSkill_")) //부가효과 처리.
            {
                cardAddSkillCell.gameObject.SetActive(true);
                cardAddSkillCell.UpdateData(data.Id.Split("part_AddSkill_")[1]);
            }
            else
            {
                itemIcon.gameObject.SetActive(true);
                nameText.text = data.Id;
            }
            priceText.text = data.Count.ToString() + LocalizationManager.GetTranslation("gold");;
        }
    }
}