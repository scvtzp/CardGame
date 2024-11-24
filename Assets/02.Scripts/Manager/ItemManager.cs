using DefaultNamespace;
using Manager.Generics;
using UI.CardMacker;
using UI.StageReward;
using UnityEngine;

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
                PlayerModel.Instance.Gold.Value -= data.Count; //todo: 카운트가 아니라 price여야할거같은데?
            }
        }

        public void SetPart(CardData data)
        {
            if (_itemData == null)
            {
                Debug.LogError("SetPart 실패. _itemData가 null입니다.");
                return;
            }
            data.Set(_itemData.Id);
            
            if (data.isSetDone())
            {
                GameManager.Instance.playerDeck.AddCard(data);
                data.Reset();
            }
            _itemData = null;
        }
    }
}