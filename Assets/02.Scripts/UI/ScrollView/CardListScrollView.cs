using DefaultNamespace;
using UnityEngine;

namespace UI.ScrollView
{
    public class CardListScrollView : LoopScrollView<MonoBehaviour, CardData>
    {
        public override void ProvideData(Transform cellTransform, int idx)
        {
            cellTransform.GetComponent<CardView>().UpdateData(DataList[idx]);
        }
    }
}