using UI.ScrollView;
using UnityEngine;

namespace UI.StageReward
{
    public class StageRewardScrollView : LoopScrollView<StageRewardCell, ItemData>
    {
        private StageRewardPresenter _presenter;

        public void PreInit(StageRewardPresenter presenter)
        {
            _presenter = presenter;
        }
        
        public override void ProvideData(Transform cellTransform, int idx)
        {
            var cell = cellTransform.GetComponent<StageRewardCell>(); 
            cell.UpdateData(DataList[idx]);
            cell.ReSetButton(()=>_presenter.GetReward(idx));
        }
    }
}