using System.Collections.Generic;
using DefaultNamespace;
using Manager;

namespace UI.StageReward
{
    public class StageRewardPresenter
    {
        private readonly StageRewardView _view;

        private List<ItemData> _rewards; 
        
        public StageRewardPresenter(StageRewardView view)
        {
            _view = view;
        }
        
        public void Init()
        {
            var rewards = GetRewardList();
            _view.UpdateData(rewards);
        }

        //todo : 데이터 로직 추가.
        private List<ItemData> GetRewardList()
        {
            _rewards = new List<ItemData>();
            _rewards.Add(new ItemData("A", -100));
            _rewards.Add(new ItemData("part_card_0", 1));
            _rewards.Add(new ItemData("part_cost_0", 1));
            _rewards.Add(new ItemData("D", 1));
            
            return _rewards;
        }

        public void GetReward(int index)
        {
            ItemManager.Instance.GetItem(_rewards[index]);
            ViewManager.Instance.HideView<StageRewardView>();
        }
    }
}