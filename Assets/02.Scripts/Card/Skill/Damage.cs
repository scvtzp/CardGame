using CardGame.Entity;
using DefaultNamespace;

namespace Skill
{
    /// <summary>
    /// Value 0 : 딜량 (음수면 힐됨)
    /// </summary>
    public class Damage : ISkill
    {
        public Damage() { }
        public Damage(int[] value) : base(value) { }
        public Damage(TargetType targetType, params int[] value) : base(targetType, value) { }
        
        public override void StartSkill(Entity target)
        {
            target.ChangeHp(-Values[0]);
        }
        
        public override ISkill Clone()
        {
            return new Damage(Target, Values);
        }
    }
}