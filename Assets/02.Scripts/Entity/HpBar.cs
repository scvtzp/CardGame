using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CardGame.Entity
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] Transform fillTransform;
        [SerializeField] TextMeshPro text;

        public async UniTask SetHpBar(int curHp, int maxHp, bool isAnimated = true)
        {
            float fillAmount = (float)curHp / maxHp;
            if (fillAmount < 0) fillAmount = 0;

            if(isAnimated)
                await fillTransform.DOScaleX(fillAmount, 0.5f).SetEase(Ease.OutQuint).ToUniTask();
            else
                fillTransform.localScale = new Vector3(fillAmount, 1, 1);
            
            text.text = $"{curHp}/{maxHp}";
        }
    }
}