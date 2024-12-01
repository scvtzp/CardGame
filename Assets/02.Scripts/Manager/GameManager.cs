using System.Collections.Generic;
using _02.Scripts.Manager;
using DefaultNamespace;
using CardGame.Entity;
using UnityEngine;
using Manager.Generics;
using Skill;

namespace Manager
{
    public enum TriggerType
    {
        TurnStart,
        TurnEnd,
        UseCardStart,
        UseCardEnd,
        GetDamage,
        GetHeal,
    }

    public enum Turn
    {
        MyTurn,
        EnemyTurn,
    }

    public class TriggerData
    {
        public string userId; //사용자의 이름
        
        //ex) 죄악의 낙인
        //적 하수인에게 낙인을 부여합니다. 그 하수인이 피해를 받을 때마다 적 영웅에게 피해를 1 줍니다.
        public TriggerType triggerType; //트리거 - 피해를 받을 때마다.
        public TargetObject triggerSource; //트리거 대상 - 그 하수인이.
        public TargetObject target; //행동 대상 - 적 영웅에게.
        public List<ISkill> skills; // 행동 - 피해를 1 줍니다.

        public int count; //언제까지 유지되는것인가? -> -1이면 평생.

        public TriggerData(TriggerType triggerType, TargetType triggerSource, TargetType target, int count, List<ISkill> skills)
        {
            this.triggerType = triggerType;
            this.triggerSource = new TargetObject(triggerSource);
            this.target = new TargetObject(target);
            this.skills = skills;
            this.count = count;
        }

        /// <summary>
        /// 주의!! count를 제외하고는 얕은 복사중임. 상관없어서 일단냅둠.
        /// </summary>
        /// <param name="triggerData"></param>
        public TriggerData(TriggerData triggerData)
        {
            this.triggerType = triggerData.triggerType;
            this.triggerSource = triggerData.triggerSource;
            this.target = triggerData.target;
            this.skills = triggerData.skills;
            this.count = triggerData.count;
        }
    }

    public class GameManager : Singleton<GameManager>
    {
        private LinkedList<DeckService> decks = new();
        public DeckService playerDeck;
        
        public Turn Turn { get; private set; } = Turn.MyTurn;
        public List<Entity> team1Entity = new List<Entity>(); //아군 캐릭터들
        public List<Entity> team2Entity = new List<Entity>(); //적 캐릭터들
        
        public GameManager()
        {
            
        }
        
        public void AddDeck(DeckService deck) => decks.AddLast(deck);
        public void AddEntity(Entity entity, TargetType type)
        {
            //개체 생성시 델리게이트 추가?
            if(type.HasFlag(TargetType.Ally))
                team1Entity.Add(entity);
            if(type.HasFlag(TargetType.Enemy))
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
            TriggerManager.Instance.OnTrigger(TriggerType.UseCardStart);
            
            foreach (var skill in card.GetSkill()) //카드 사용
                skill.StartSkill(target);
            
            // 덱에서 버린 더미로 이동
            foreach (var deck in decks)
            {
                if(deck.ContainsCard(card._cardData))
                    deck.UseCard(card);
            }
            //todo: 버린더미로 이동될 때 ~~~ 로직 추가
            
            TriggerManager.Instance.OnTrigger(TriggerType.UseCardEnd);
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
            TriggerManager.Instance.OnTrigger(TriggerType.TurnEnd);
            
            Turn = Turn == Turn.MyTurn ? Turn.EnemyTurn : Turn.MyTurn;
            
            StartTurn();
            TriggerManager.Instance.OnTrigger(TriggerType.TurnStart);
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

