using System.Collections.Generic;
using _02.Scripts.Manager;
using DefaultNamespace;
using CardGame.Entity;
using DG.Tweening;
using UnityEngine;
using Manager.Generics;
using Skill;
using UI.TurnChangeView;

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
        //todo : 임시용. 나중에 팝업으로 따로 빼기
        [SerializeField] private CardView cardView;
        
        private LinkedList<DeckService> decks = new();
        public DeckService playerDeck;
        
        public Turn Turn { get; private set; } = Turn.MyTurn;
        public List<Entity> team1Entity = new List<Entity>(); //아군 캐릭터들
        public List<Entity> team2Entity = new List<Entity>(); //적 캐릭터들

        public int turnCount = 0; //처음엔 0턴이 맞고, 1턴이 되면서(턴 시작 액션 되면서) 게임 시작하는게 맞음.
        
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
        /// //todo: 삭제하고 아래의 CardData기반으로 통폐합.
        public void Action(Card card, Entity target)
        {
            TriggerManager.Instance.OnTrigger(TriggerType.UseCardStart);

            foreach (var skill in card.GetSkill()) //카드 사용
            {
                if (!skill.NeedSelectTarget)
                    skill.StartSkill(GetTarget(skill.Target));
                else
                    skill.StartSkill(target);
            }
            
            // 덱에서 버린 더미로 이동
            foreach (var deck in decks)
            {
                if(deck.ContainsCard(card._cardData))
                    deck.UseCard(card);
            }
            //todo: 버린더미로 이동될 때 ~~~ 로직 추가
            
            TriggerManager.Instance.OnTrigger(TriggerType.UseCardEnd);
        }
        
        /// <summary>
        /// 오토용. 사실 Card를 매개변수로 넘겨주는게 말이안된다. 일반 로직도 CardData로 수정.
        /// </summary>
        /// <param name="cardData"></param>
        public async void Action(CardData cardData)
        {
            TriggerManager.Instance.OnTrigger(TriggerType.UseCardStart);
            
            await cardView.UpdateData(cardData);
            cardView.gameObject.SetActive(true);
            DOVirtual.DelayedCall(0.45f, () => 
            {
                cardView.gameObject.SetActive(false);
            });

            foreach (var skill in cardData.GetSkill()) //카드 사용
            {
                if (!skill.NeedSelectTarget)
                    skill.StartSkill(GetTarget(skill.Target));
                else
                    skill.StartSkill(GetTarget(cardData._costAndTarget._targetType));
            }
            
            // 덱에서 버린 더미로 이동
            foreach (var deck in decks)
            {
                if(deck.ContainsCard(cardData))
                    deck.UseCard(cardData);
            }
            //todo: 버린더미로 이동될 때 ~~~ 로직 추가
            
            TriggerManager.Instance.OnTrigger(TriggerType.UseCardEnd);
        }

        private Entity GetTarget(TargetType targetType) => new TargetObject(targetType).GetTarget()[0]; 
            
        public async void ChangeTurn()
        {
            TriggerManager.Instance.OnTrigger(TriggerType.TurnEnd);
            
            Turn = Turn == Turn.MyTurn ? Turn.EnemyTurn : Turn.MyTurn;

            if(Turn == Turn.MyTurn)
                turnCount++;
            
            //todo: TurnChangeView 애니메이션 끝나야 턴 바뀌었으면 좋겠음. 
            ViewManager.Instance.ShowView<TurnChangeView>();
            
            TriggerManager.Instance.OnTrigger(TriggerType.TurnStart);
            // 적군은 자동턴.
            if (Turn == Turn.EnemyTurn)
            {
                foreach (var entity in team2Entity)
                    await entity.AutoTurn();
                
                //끝나면 자동으로 턴 넘김
                ChangeTurn();
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
                turnCount = 0;
                
                StageManager.Instance.ChangeStage();
            }
        }
    }
}

