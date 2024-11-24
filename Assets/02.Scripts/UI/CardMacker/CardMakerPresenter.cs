using DefaultNamespace;
using Manager;
using R3;

namespace UI.CardMacker
{
    public class CardMakerPresenter
    {
        private CardMakerView _view;

        //todo: 유물 등으로 이 카드 생성 갯수 조절 필요
        private ReactiveProperty<int> AAAtodoAAA = new ReactiveProperty<int>(2);
        
        public CardMakerPresenter(CardMakerView view) 
        {
            _view = view;
        }

        public void Init()
        {
            AAAtodoAAA.Subscribe(_view.MakeCard); //카드 생성 갯수 유물 등으로 인해 변경시 자동으로 늘어나게끔.
        }
        
        public void SetCard(CardData data)
        {
            ItemManager.Instance.SetPart(data);
            _view.UpdateData();
        }
    }
}