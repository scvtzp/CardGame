using System.Collections.Generic;
using System.Linq;
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
        public readonly string CardId; //카드 자체 id
        public CostAndTarget _costAndTarget { get; private set; }

        private List<ISkill> _skill = new();
        //todo: 부가효과 도대체 어케함?

        public CardData()
        {
        }
        
        public CardData(int cost, TargetType targetType, List<ISkill> skill, string cardId)
        {
            _costAndTarget = new CostAndTarget(cost, targetType);
            _skill = skill;
            CardId = cardId;
        }

        public CardData(CardData cardData)
        {
            id = CardIDManager.Instance.GetInstanceID();
            _costAndTarget = new CostAndTarget(cardData._costAndTarget);
            _skill = cardData._skill.Select(skill => skill.Clone()).ToList();
            CardId = cardData.CardId;
        }
        
        public bool GetTarget(Entity target, ObjectType objectType)
        {
            return _costAndTarget.GetTarget(target, objectType);
        }

        public List<ISkill> GetSkill() => _skill;
    }
}