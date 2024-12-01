using System;
using System.Collections.Generic;
using CardGame.Entity;
using DefaultNamespace;
using Manager;
using Manager.Generics;

namespace _02.Scripts.Manager
{
    public class TriggerManager : Singleton<TriggerManager>
    {
        // 델리게이트 저장이 아니라 카드 데이터 저장으로 바꿈. 
        // 사유: 주문공격력 실시간 적용 등 델리게이트로 박기엔 관리에 어려움이 있음.
        private Dictionary<TriggerType, List<TriggerData>> action = new(); 
        
        public void AddAction(TriggerData data)
        {
            if (!action.ContainsKey(data.triggerType))
                action.Add(data.triggerType, new List<TriggerData>());
            
            action[data.triggerType].Add(data);
        }
        
        public void OnTrigger(TriggerType triggerType, Entity triggerSource = null)
        {
            // 다 똑같긴 한데.. 언젠가는 분명 따로 나눌 일이 생길듯?
            switch(triggerType)
            {
                case TriggerType.TurnStart:
                    StartSkill(triggerType, triggerSource);
                    break;
                case TriggerType.TurnEnd:
                    StartSkill(triggerType, triggerSource);
                    break;
                case TriggerType.UseCardStart:
                    StartSkill(triggerType, triggerSource);
                    break;
                case TriggerType.UseCardEnd:
                    StartSkill(triggerType, triggerSource);
                    break;
                case TriggerType.GetDamage:
                    StartSkill(triggerType, triggerSource);
                    break;
                case TriggerType.GetHeal:
                    StartSkill(triggerType, triggerSource);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(triggerType), triggerType, null);
            }
        }

        private void StartSkill(TriggerType triggerType, Entity triggerSource = null)
        {
            if (!action.ContainsKey(triggerType))
                return;
            
            foreach (var triggerData in action[triggerType])
            {
                triggerData.count -= 1; //제한시간 감소. 
                
                //null이면 애초에 특정 source에 엮여있지 않은 애들. ex) 턴종료 시 발동 같은거.
                if(triggerSource != null && triggerSource.type != triggerData.triggerSource.Type)
                    continue;
                
                foreach (var skill in triggerData.skills)
                    foreach (var entity in triggerData.target.GetTarget())
                        skill.StartSkill(entity);
            }
            
            action[triggerType].RemoveAll(triggerData => triggerData.count == 0);
        }
    }
}