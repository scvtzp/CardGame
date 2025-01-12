using System.Collections.Generic;
using _02.Scripts.Manager;
using CardGame.Entity;
using DefaultNamespace;
using Manager;
using Skill;

namespace AddSkill
{
    public class LifeSteal : IAddSkill
    {
        //미완.
        // TargetType.Me를 어떻게 해야하나 고민중.
        // 지금은 무조건 플레이어 인데... 이러면 적이 사용이 불가능하다.
        // 어디선가 사용하는 주체도 추가로 기록해야하나?
        public void AddSkillStart(Entity entity) 
        {
            List<ISkill> skillList = new List<ISkill>();
            skillList.Add(new Heal(new int[] { 5 }));
            TargetType allFlags = (TargetType)~TargetType.None;
            TriggerData data = new TriggerData(TriggerType.GetDamage, allFlags, TargetType.Me, -1, skillList);
            TriggerManager.Instance.AddAction(data);
        }

        public void AddSkillStart(Entity entity, CardData cardData)
        {
            throw new System.NotImplementedException();
        }

        public void AddSkillEnd()
        {
            //Start에서 추가한 트리거를 여기서 해제해주면 될듯?
        }
    }
}