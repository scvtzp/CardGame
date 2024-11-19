using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI type;
        [SerializeField] private TextMeshProUGUI effect;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private TextMeshProUGUI target;

        public void UpdateData(CardData cardData)
        {
            // todo: 카드의 "이름"필요. ex)흡혈충(Damage(3)/Heal(3)) 이런식으로 매핑 필요.
            // 이름으로 이미지와 타이틀 지정.
            // 타입은 슬더스 형식이라 일단 뒀는데 아마 나중에 빠지지 않을까? 싶네.
            
            title.text = cardData.CardId;

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
                    effectStr += nameof(skill);
            }
            effect.text = effectStr;
        }
    }
}