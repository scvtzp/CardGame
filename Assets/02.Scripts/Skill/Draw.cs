using CardGame.Entity;
using DefaultNamespace;

namespace Skill
{
    public class Draw : ISkill
    {
        public Draw() { }
        public Draw(int[] value) : base(value) { }
        public Draw(TargetType targetType, params int[] value) : base(targetType, value) { }
        
        public override void StartSkill(Entity target)
        {
            target.Draw(Values[0]);
        }

        public override ISkill Clone()
        {
            return new Draw(Target, Values);
        }
    }
}