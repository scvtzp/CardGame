using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DeckService : MonoBehaviour
    {
        private LinkedList<Card> _deck = new ();
        public List<Card> Hand { get; private set; }  = new();
        public List<Card> Trash { get; private set; } = new ();
        private HashSet<double> _allCardID = new ();

        public int DrawCount = 0;
        
        //todo: 핸드에 오브젝트 풀 적용시켜야함
        public void Draw()
        {
            if (_deck.Count == 0)
            {
                if(Trash.Count == 0) //덱도 없고 버린더미도 없는 경우
                    return;
                
                foreach (var VARIABLE in Trash)
                    _deck.AddLast(VARIABLE);
                
                Trash.Clear();
            }
            
            Hand.Add(_deck.First.Value);
            _deck.RemoveFirst();
        }

        public void StartDraw()
        {
            for (int i = 0; i < DrawCount; i++)
                Draw();
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
            Hand.Remove(card);
            Trash.Add(card);
        }

        public bool ContainsCard(Card card) => _allCardID.Contains(card.id);
    }
}