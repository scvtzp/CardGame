using UnityEngine;

namespace CardGame.Entity
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] Transform fillTransform;

        public void SetHpBar(int curHp, int maxHp)
        {
            float fillAmount = (float)curHp / maxHp;
            if (fillAmount < 0) fillAmount = 0;
            
            fillTransform.localScale = new Vector3(fillAmount, 1, 1);
        }
    }
}