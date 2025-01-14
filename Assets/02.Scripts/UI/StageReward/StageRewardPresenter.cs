using System.Collections.Generic;
using System.Linq;
using AddSkill;
using DefaultNamespace;
using Manager;

namespace UI.StageReward
{
    public class StageRewardPresenter
    {
        private readonly StageRewardView _view;

        private List<ItemData> _rewards; 
        private readonly List<string> _cardCostKeys; 
        private readonly List<string> _cardBodyKeys; 
        private readonly List<IAddSkill> _cardAddSkillKeys; 
        
        public StageRewardPresenter(StageRewardView view)
        {
            _view = view;
            _cardCostKeys = DefaultDeckManager.Instance.cardCost.Keys.ToList();
            _cardBodyKeys = DefaultDeckManager.Instance.cardBody.Keys.ToList();
            _cardAddSkillKeys = DefaultDeckManager.Instance.cardAddSkill;
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
            _rewards.Add(new ItemData($"part_{_cardBodyKeys.GetRandomElement()}", 1));
            _rewards.Add(new ItemData($"part_{_cardCostKeys.GetRandomElement()}", 1));
            _rewards.Add(new ItemData($"part_AddSkill_{_cardAddSkillKeys.GetRandomElement().GetType().Name}", 1));
            
            return _rewards;
        }

        public void GetReward(int index)
        {
            ItemManager.Instance.GetItem(_rewards[index]);
            ViewManager.Instance.HideView<StageRewardView>(); 
        }
    }
}