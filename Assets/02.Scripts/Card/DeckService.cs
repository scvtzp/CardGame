using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeckService : MonoBehaviour
    {
        private LinkedList<Card> _deck = new ();
        private List<Card> _hand = new ();
        private List<Card> _trash = new ();
        private HashSet<double> _allCardID = new ();
        
        //todo: 핸드에 오브젝트 풀 적용시켜야함
        public void Draw()
        {
            if (_deck.Count == 0)
            {
                if(_trash.Count == 0) //덱도 없고 버린더미도 없는 경우
                    return;
                
                foreach (var VARIABLE in _trash)
                    _deck.AddLast(VARIABLE);
                
                _trash.Clear();
            }
            
            _hand.Add(_deck.First.Value);
            _deck.RemoveFirst();
        }

        public void AddCard(Card card)
        {
            _allCardID.Add(card.id);
            _deck.AddLast(card);
            GameUtil.ShuffleCollection<Card>(_deck);
        }
        
        public void UseCard(Card card)
        {
            card.UsedCard();
            _hand.Remove(card);
            _trash.Add(card);
        }

        public bool ContainsCard(Card card) => _allCardID.Contains(card.id);
    }
}