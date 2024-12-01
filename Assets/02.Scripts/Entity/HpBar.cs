using TMPro;
using UnityEngine;

namespace CardGame.Entity
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] Transform fillTransform;
        [SerializeField] TextMeshPro text;

        public void SetHpBar(int curHp, int maxHp)
        {
            float fillAmount = (float)curHp / maxHp;
            if (fillAmount < 0) fillAmount = 0;
            
            fillTransform.localScale = new Vector3(fillAmount, 1, 1);
            text.text = $"{curHp}/{maxHp}";
        }
    }
}