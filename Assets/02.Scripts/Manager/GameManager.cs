using System.Collections.Generic;
using DefaultNamespace;
using CardGame.Entity;
using UnityEngine;

namespace Manager
{
    public delegate void KillDelegate();
    public delegate void VoidDelegate();

    public enum KillType
    {
        Ally,
        Enemy
    }
    public enum SkillDelegateType
    {
        Start,
        End,
    }

    public class GameManager : Singleton<GameManager>
    {
        private Dictionary<KillType, KillDelegate> KillDelegates = new();
        private Dictionary<SkillDelegateType, VoidDelegate> SkillDelegate = new();

        private LinkedList<DeckService> decks = new();
        
        public GameManager()
        {
            SkillDelegate.Add(SkillDelegateType.Start, null);
            SkillDelegate.Add(SkillDelegateType.End, null);
        }
        
        public void AddSkillDelegate(SkillDelegateType type, VoidDelegate del) => SkillDelegate[SkillDelegateType.Start] += del;
        public void ResetSkillDelegate(SkillDelegateType type) => SkillDelegate[SkillDelegateType.Start] = null;
        public void AddDeck(DeckService deck) => decks.AddLast(deck);
        
        public void KillAction(Entity obj)
        {
            if(decks.Contains(obj.deck))
                decks.Remove(obj.deck);

            obj.Kill();
            //KillDelegates[killType].Invoke();
        }

        public void Action(Card card, Entity target)
        {
            SkillDelegate[SkillDelegateType.Start]?.Invoke();
            
            foreach (var skill in card.GetSkill()) //카드 사용
                skill.StartSkill(target);
            
            // 덱에서 버린 더미로 이동
            foreach (var deck in decks)
            {
                if(deck.ContainsCard(card))
                    deck.UseCard(card);
            }
            //todo: 버린더미로 이동될 때 ~~~ 로직 추가

            SkillDelegate[SkillDelegateType.End]?.Invoke();
        }
    }
}

