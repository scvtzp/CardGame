using System.Collections.Generic;
using System.Linq;
using AddSkill;
using CardGame.Entity;
using Manager;
using Skill;

namespace DefaultNamespace
{
    // mono를 제거하기 위해 Data에는 카드의 데이터만 저장함.
    // 드래그하고 사용하고 이런 액션은 Card 클래스에서 처리.
    public class CardData
    {
        public double id; //순차적으로 1씩 늘어나는 객체별 고유 id
        public string CardId; //카드 자체 id
        public CostAndTarget _costAndTarget { get; private set; }
        private List<ISkill> _skill = new();
        private IAddSkill _addSkill;

        public CardData()
        {
            Reset();
        }
        
        public CardData(int cost, TargetType targetType, List<ISkill> skill, string cardId)
        {
            _costAndTarget = new CostAndTarget(cost, targetType);
            _skill = skill;
            CardId = cardId;
            _addSkill = null;
        } 
        
        public CardData(int cost, TargetType targetType, List<ISkill> skill, string cardId, IAddSkill addSkill)
        {
            _costAndTarget = new CostAndTarget(cost, targetType);
            _skill = skill;
            CardId = cardId;
            _addSkill = addSkill;
        }

        public CardData(CardData cardData)
        {
            id = CardIDManager.Instance.GetInstanceID();
            _costAndTarget = new CostAndTarget(cardData._costAndTarget);
            _skill = cardData._skill.Select(skill => skill.Clone()).ToList();
            CardId = cardData.CardId;
            _addSkill = cardData._addSkill;
        }

        public CardData(CostAndTarget costAndTarget, List<ISkill> skill)
        {
            id = CardIDManager.Instance.GetInstanceID();
            _addSkill = null;
            _skill = skill;
            _costAndTarget = costAndTarget;
        }

        public void Reset()
        {
            CardId = "";
            _costAndTarget = null;
            _skill = null;
            _addSkill = null;
        }
        
        public bool GetTarget(Entity target, TargetType objectType)
        {
            return _costAndTarget.CheckTarget(target, objectType);
        }

        //todo: 덮어쓰기 할건지 물어보기.
        public void Set(CostAndTarget target)
        {
            _costAndTarget = target;
        }
        public void Set(List<ISkill> skill)
        {
            _skill = skill;
        }

        public void Set(string id)
        {
            id = id.Replace("part_", "");

            if (DefaultDeckManager.Instance.cardBody.ContainsKey(id))
            {
                CardId = id;
                Set(DefaultDeckManager.Instance.cardBody[id]);
            }
            if(DefaultDeckManager.Instance.cardCost.ContainsKey(id))
                Set(DefaultDeckManager.Instance.cardCost[id]);
        }
        
        public bool isSetDone() => _costAndTarget != null && _skill != null;

        public List<ISkill> GetSkill() => _skill;
        public IAddSkill GetAddSkill() => _addSkill;
    }
}