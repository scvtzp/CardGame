using CardGame.Entity;
using DefaultNamespace;

namespace Skill
{
    /// <summary>
    /// 데미지에 -하면 힐이지만, 가독성을 위해서 그냥 클래스 나눔.
    /// Value 0 : 힐량
    /// </summary>
    public class Heal : ISkill
    {
        public Heal() { }
        public Heal(int[] value) : base(value) { }
        public Heal(TargetType targetType, params int[] value) : base(targetType, value) { }
        
        public override void StartSkill(Entity target)
        {
            target.ChangeHp(Values[0]);
        }
        
        public override ISkill Clone()
        {
            return new Heal(Target, Values);
        }
    }
}