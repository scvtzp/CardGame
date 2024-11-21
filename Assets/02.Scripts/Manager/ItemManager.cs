using DefaultNamespace;
using Manager.Generics;
using UI.CardMacker;
using UI.StageReward;

namespace Manager
{
    public class ItemManager : Singleton<ItemManager>
    {
        private ItemData _itemData;
        
        public void GetItem(ItemData data)
        {
            if (data.Id.Contains("part"))
            {
                _itemData = data;
                ViewManager.Instance.ShowView<CardMakerView>();
            }
            else
            {
                PlayerModel.Instance.Gold.Value -= data.Count;
            }
        }

        public void SetPart(CardData data)
        {
            data.Set(DefaultDeckManager.Instance.cardBody[_itemData.Id]);
        }
    }
}