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

        private List<CardData> _cardDatas = new List<CardData>();
        private List<CardView> _cardViews = new List<CardView>();
        
        //todo: 유물 등으로 이 카드 생성 갯수 조절 필요
        private int AAAtodoAAA = 2;

        public override void Init()
        {
            _cardViews.Clear();
            for (int i = 0; i < AAAtodoAAA; i++)
            {
                _cardViews.Add(Instantiate(cardView, cardParentTransform));
                _cardViews[i].gameObject.AddComponent<Button>().onClick.AddListener(AddData);
                _cardDatas.Add(new CardData());
            }
        }

        private void AddData()
        {
            
        }
        
        public void UpdateData()
        {
            for (int i = 0; i < AAAtodoAAA; i++)
                _cardViews[i].UpdateData(_cardDatas[i]);
        }
    }
}