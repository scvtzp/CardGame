using System.Collections.Generic;
using DefaultNamespace;
using UI.UIBase;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CardMacker
{
    public class CardMakerView : ViewBase
    {
        [SerializeField] private CardView cardView;
        [SerializeField] private RectTransform cardParentTransform;

        private CardMakerPresenter _presenter;
        
        private List<CardData> _cardDatas = new List<CardData>();
        private List<CardView> _cardViews = new List<CardView>();

        public override void Init()
        {
            _presenter = new CardMakerPresenter(this);
            
            _cardViews.Clear();
        }

        public void MakeCard(int count)
        {
            for (int i = _cardViews.Count; i < count; i++)
            {
                int index = i; //람다 클로저(closure) 문제 방지용 변수
                _cardDatas.Add(new CardData());
                _cardViews.Add(Instantiate(cardView, cardParentTransform));
                _cardViews[i].gameObject.AddComponent<Button>().onClick.AddListener(()=>_presenter.SetCard(_cardDatas[index]));
            }
        }

        public override void ShowStart()
        {
            UpdateData();
        }
        
        public void UpdateData()
        {
            for (int i = 0; i < _cardViews.Count; i++)
                _cardViews[i].UpdateData(_cardDatas[i]);
        }
    }
}