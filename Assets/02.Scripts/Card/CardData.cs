using System.Collections.Generic;
using System.Linq;
using CardGame.Entity;
using Manager;

namespace DefaultNamespace
{
    // mono를 제거하기 위해 Data에는 카드의 데이터만 저장함.
    // 드래그하고 사용하고 이런 액션은 Card 클래스에서 처리.
    public class CardData
    {
        public double id; //순차적으로 1씩 늘어나는 고유 id
        private CostAndTarget _costAndTarget;
        private List<ISkill> _skill = new List<ISkill>()
        {
            new Damage(),
            new Damage(),
            new Heal(),
        };
        //todo: 부가효과 도대체 어케함?

        public CardData()
        {
        }
        
        public CardData(int cost, TargetType targetType, List<ISkill> skill)
        {
            _costAndTarget = new CostAndTarget(cost, targetType);
            _skill = skill;
        }

        public CardData(CardData cardData)
        {
            id = CardIDManager.Instance.GetInstanceID();
            _costAndTarget = new CostAndTarget(cardData._costAndTarget);
            _skill = cardData._skill.Select(skill => skill.Clone()).ToList();
        }
        public bool GetTarget(Entity target, ObjectType objectType)
        {
            return _costAndTarget.GetTarget(target, objectType);
        }

        public List<ISkill> GetSkill() => _skill;
    }
}