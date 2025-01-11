using System.Collections.Generic;
using _02.Scripts.Manager;
using CardGame.Entity;
using DefaultNamespace;
using Manager;

namespace Skill
{
    public class TriggerAction : ISkill 
    {
        public TriggerAction(TriggerType triggerType, TargetType triggerSource, TargetType target, List<ISkill> skills, params int[] value) : base(value)
        {
            _triggerData = new TriggerData(triggerType, triggerSource, target, Values[0], skills);
        }
        
        public TriggerAction(TargetType targetType, TriggerType triggerType, TargetType triggerSource, TargetType target, List<ISkill> skills, params int[] value) : base(targetType, value)
        {
            _triggerData = new TriggerData(triggerType, triggerSource, target, Values[0], skills);
        }

        public TriggerAction(TargetType targetType, TriggerData triggerData, params int[] value) : base(targetType, value)
        {
            _triggerData = new TriggerData(triggerData);
        }

        private TriggerData _triggerData;
        
        public override void StartSkill(Entity target)
        {
            TriggerManager.Instance.AddAction(_triggerData);
        }

        public override ISkill Clone()
        {
            return new TriggerAction(Target, _triggerData, Values);
        }
    }
}