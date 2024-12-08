using System.Collections.Generic;
using System.Threading.Tasks;
using _02.Scripts.Manager;
using I2.Loc;
using Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    /// <summary>
    /// 일부 요소가 없더라도 작동할 수 있도록 설계하였음.
    /// </summary>
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Localize title;
        [SerializeField] private TextMeshProUGUI type; // 타입은 슬더스 형식이라 일단 뒀는데 아마 나중에 빠지지 않을까? 싶네.
        [SerializeField] private Localize desc;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI target;
        [SerializeField] private Image backEffect;

        public async Task UpdateData(CardData cardData)
        {
            await UpdateData(cardData.CardId);
            UpdateData(cardData._costAndTarget);
        }

        public async void UpdateData(CostAndTarget costAndTarget)
        {
            if (cost != null && target != null)
            {
                if (costAndTarget != null)
                {
                    cost.text = costAndTarget._cost.ToString();
                    target.text = costAndTarget._targetType.ToString();
                }
                else
                {
                    cost.text = "";
                    target.text = "";
                }
            }
        }
        
        public async Task UpdateData(string bodyId)
        {
            if(image != null)
                image.sprite = await SpriteManager.Instance.GetSpriteAsync(bodyId);
            if(title != null)
                title.SetTermAndRefresh(bodyId);
            if(desc != null)
                desc.SetTermAndRefresh($"desc_{bodyId}");
        }

        public void SetBackEffect(bool check, Color color)
        { 
            backEffect.color = color;
            SetBackEffect(check);
        }
        public void SetBackEffect(bool check)
        { 
            backEffect.gameObject.SetActive(check);
        }
    }
}