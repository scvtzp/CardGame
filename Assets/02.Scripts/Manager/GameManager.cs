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

    public enum Turn
    {
        MyTurn,
        EnemyTurn,
    }

    public class GameManager : Singleton<GameManager>
    {
        private Dictionary<KillType, KillDelegate> KillDelegates = new();
        private Dictionary<SkillDelegateType, VoidDelegate> SkillDelegate = new();

        private LinkedList<DeckService> decks = new();
        
        public Turn Turn { get; private set; } = Turn.MyTurn;
        private List<Entity> team1Entity = new List<Entity>(); //아군 캐릭터들
        private List<Entity> team2Entity = new List<Entity>(); //적 캐릭터들
        
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

        private void EndTurn() { }

        private void StartTurn()
        { 
            //todo: 스타트/엔드 둘다 카드 액션처럼 델리게이트로 변환.
        }
            
        public void ChangeTurn()
        {
            Turn = Turn == Turn.MyTurn ? Turn.EnemyTurn : Turn.MyTurn;

            StartTurn();
            // 적군은 자동턴.
            if (Turn == Turn.EnemyTurn)
            {
                foreach (var entity in team2Entity)
                    entity.AutoTurn();
                
                EndTurn();
                ChangeTurn(); //오토 턴 끝나면 자동으로 턴넘김.
            }
            //나는 그냥 냅둠. todo: 드로우는 시켜주자.
            
            EndTurn();
        }
    }
}

