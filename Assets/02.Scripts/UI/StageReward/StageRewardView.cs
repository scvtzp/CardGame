using System;
using System.Collections.Generic;
using UI.UIBase;
using UnityEngine;

namespace UI.StageReward
{
    public class StageRewardView : ViewBase
    {
        [SerializeField] private StageRewardScrollView scrollView;

        private StageRewardPresenter _presenter;

        public override void Init()
        {
            _presenter = new StageRewardPresenter(this);
            scrollView.PreInit(_presenter);
        }
        
        public override void ShowStart()
        {
            base.ShowStart();
            _presenter.Init();
        }

        public void UpdateData(List<ItemData> rewards)
        {
            scrollView.Init(rewards);
        }
    }
}