using _02.Scripts.Manager;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Localize title;
        [SerializeField] private TextMeshProUGUI type; // 타입은 슬더스 형식이라 일단 뒀는데 아마 나중에 빠지지 않을까? 싶네.
        [SerializeField] private TextMeshProUGUI effect;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI target;

        public async void UpdateData(CardData cardData)
        {
            image.sprite = await SpriteManager.Instance.GetSpriteAsync(cardData.CardId);
            
            title.SetTerm(cardData.CardId);
            title.OnLocalize(); // 즉시 업데이트

            if (cardData._costAndTarget != null)
            {
                cost.text = cardData._costAndTarget._cost.ToString();
                target.text = cardData._costAndTarget._targetType.ToString();
            }
            else
            {
                cost.text = "";
                target.text = "";
            }

            string effectStr = string.Empty;
            if (cardData.GetSkill() != null)
            {
                foreach (var skill in cardData.GetSkill())
                    effectStr += skill.GetType().Name +"\n";
            }
            effect.text = effectStr;
        }
    }
}