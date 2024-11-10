using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private Card cardObj;

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
            SetPos();
        }

        public void SetPos()
        {
            for (var index = 0; index < transform.childCount; index++)
            {
                var childTransform = transform.GetChild(index).GetComponent<RectTransform>();
                childTransform.anchoredPosition = new Vector2((index-transform.childCount/2)*100,134);
            }
        }
    }
}