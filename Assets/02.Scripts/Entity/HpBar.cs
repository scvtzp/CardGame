using DG.Tweening;
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

            fillTransform.DOScaleX(fillAmount, 0.5f).SetEase(Ease.OutQuint);
            text.text = $"{curHp}/{maxHp}";
        }
    }
}