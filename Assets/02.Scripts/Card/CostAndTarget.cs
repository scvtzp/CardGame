using CardGame.Entity;

namespace DefaultNamespace
{
    public class CostAndTarget
    {
        public int _cost { get; private set; }
        //todo: 지금은 적을 타게하기로 되어있는데, 다양한 타겟을 경우에 따라 조준할 수 있도록 수정 필요.
        public TargetType _targetType { get; private set; }

        public CostAndTarget(int cost, TargetType targetType)
        {
            _cost = cost;
            _targetType = targetType;
        }
        
        public CostAndTarget(CostAndTarget costAndTarget)
        {
            _cost = costAndTarget._cost;
            _targetType = costAndTarget._targetType;
        }
        
        public bool CheckTarget(Entity other, TargetType mytype)
        {
            if ((other.type & _targetType) != 0) 
                return true;
            
            return false;
        }
    }
}