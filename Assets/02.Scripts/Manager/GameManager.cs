using System.Collections.Generic;
using DefaultNamespace;
using CardGame.Entity;
using UnityEngine;
using Manager.Generics;

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
        public DeckService playerDeck;
        
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
        public void AddEntity(Entity entity, ObjectType type)
        {
            //개체 생성시 델리게이트 추가?
            if(type == ObjectType.Team1 || type == ObjectType.Team1Create)
                team1Entity.Add(entity);
            if(type == ObjectType.Team2 || type == ObjectType.Team2Create)
                team2Entity.Add(entity);
        }

        public void KillAction(Entity obj)
        {
            if(decks.Contains(obj.deckService))
                decks.Remove(obj.deckService);

            if (team1Entity.Contains(obj))
                team1Entity.Remove(obj);
            else if (team2Entity.Contains(obj))
                team2Entity.Remove(obj);
            else 
                Debug.LogError(obj.name + "어떤 팀에도 속해있지 않습니다");
            
            obj.Kill();
            StageCheck();
            //KillDelegates[killType].Invoke();
        }

        /// <summary>
        /// 카드 사용 로직
        /// </summary>
        public void Action(Card card, Entity target)
        {
            SkillDelegate[SkillDelegateType.Start]?.Invoke();
            
            foreach (var skill in card.GetSkill()) //카드 사용
                skill.StartSkill(target);
            
            // 덱에서 버린 더미로 이동
            foreach (var deck in decks)
            {
                if(deck.ContainsCard(card._cardData))
                    deck.UseCard(card);
            }
            //todo: 버린더미로 이동될 때 ~~~ 로직 추가
            
            SkillDelegate[SkillDelegateType.End]?.Invoke();
        }

        public void EndTurn()
        {
            ChangeTurn();
        }

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
                return;
            }
            //나는 그냥 냅둠. todo: 드로우는 시켜주자.
            else
            {
                foreach (var entity in team1Entity)
                    entity.StartTurn();
            }
        }
        
        //스테이지 관련 코드
        public void StageCheck()
        {
            if (team1Entity.Count == 0)
            {
                Debug.LogError("플레이에 패배");
            }
            else if (team2Entity.Count == 0)
            {
                //보상 추가

                StageManager.Instance.ChangeStage();
            }
        }
    }
}

