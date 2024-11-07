using DefaultNamespace;
using UnityEngine;

namespace UI.ScrollView
{
    public class CardListScrollView : LoopScrollView<MonoBehaviour, CardData>
    {
        public override void ProvideData(Transform transform, int idx)
        {
            base.ProvideData(transform, idx);
            
            //todo: 오브젝트ID를 기반으로 캐싱 작업 추가. 
            transform.GetComponent<CardView>().UpdateData(DataList[idx]);
        }
    }
}