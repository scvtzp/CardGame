using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private Card cardObj;

        private List<Card> _cardList = new();

        private Card cardSetting = new Card();
        private DeckService _deckService;
        
        public void SetCardSetting(DeckService deck)
        {
            _deckService = deck;
            _deckService.OnDraw += AddCard;
        }
        
        public void AddCard()
        {
            Card card = Instantiate(cardObj, transform);
            card.Init(_deckService, _deckService.Hand[^1]); //어쩌피 방금 드로우 했으면 맨 뒤일거니까.
            _cardList.Add(card);
            SetPos();
        }

        public void SetPos()
        {
            for (var index = 0; index < _cardList.Count; index++)
            {
                var transform = _cardList[index].GetComponent<RectTransform>();

                transform.anchoredPosition = new Vector2((index-_cardList.Count/2)*100,134);
            }
        }
    }
}