using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeckService : MonoBehaviour
    {
        //성능 이슈로 LinkedList 고려 해봐야할듯.
        public List<CardData> _deck = new ();
        public List<CardData> Hand { get; private set; }  = new();
        public List<CardData> Trash { get; private set; } = new ();
        private HashSet<double> _allCardID = new ();

        private int _drawCount = 1;
        public Action OnDraw;
        
        //todo: 핸드에 오브젝트 풀 적용시켜야함
        public void Draw()
        {
            if (_deck.Count == 0)
            {
                if(Trash.Count == 0) //덱도 없고 버린더미도 없는 경우
                    return;
                
                foreach (var VARIABLE in Trash)
                    _deck.Add(VARIABLE);
                
                Trash.Clear();
            }
            
            Hand.Add(_deck[^1]);
            OnDraw?.Invoke();
            _deck.RemoveAt(_deck.Count - 1);
        }

        public void Draw(int count)
        {
            for (int i = 0; i < count; i++)
                Draw();
        }

        public void StartDraw()
        {
            for (int i = 0; i < _drawCount; i++)
                Draw();
        }

        public void SetDeck(List<CardData> deck)
        {
            foreach (var card in deck)
                AddCard(card);
        }
        
        public void AddCard(CardData cardData)
        {
            var card = new CardData(cardData);
            _allCardID.Add(card.id);
            _deck.Add(card);
            GameUtil.ShuffleCollection<CardData>(_deck);
        }
        
        public void UseCard(Card card)
        {
            card.UsedCard();
            Hand.Remove(card._cardData);
            Trash.Add(card._cardData);
        }

        public bool ContainsCard(CardData card) => _allCardID.Contains(card.id);
    }
}