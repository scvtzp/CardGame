using System;
using System.Collections.Generic;
using CardGame.Entity;
using Manager;
using UnityEngine;

namespace DefaultNamespace
{
    [Flags]
    public enum TargetType
    {
        None = 0, //그냥 내면 됨.
        Me = 1<<0, //본인
        Ally = 1<<1, //아군
        Enemy = 1<<2, //적군
        
        Summoner = 1<<3, //소환된 잡몹
        Select = 1<<4, //내가 따로 선택한 단일 개체.
    }
    
    public class TargetObject
    {
        public TargetType Type { get; private set; }
        private List<Entity> _selectTargetEntity = new(); //_type Select일때 사용되는 대상 저장용 Entity.

        public TargetObject(TargetType type)
        {
            Type = type;
        }

        public void AddSelectEntity(Entity entity)  
        {
            _selectTargetEntity.Add(entity);
        }
        
        //todo: 이거 메니저로 해서 따로 빼야할듯?
        public List<Entity> GetTarget()
        {
            if (Type.HasFlag(TargetType.Ally) && Type.HasFlag(TargetType.Enemy))
            {
                List<Entity> returnList = new List<Entity>();
                returnList.AddRange(GameManager.Instance.team1Entity);
                returnList.AddRange(GameManager.Instance.team2Entity);
                return returnList;
            }
            if (Type.HasFlag(TargetType.Ally))
            {
                return GameManager.Instance.team1Entity;
            }
            if (Type.HasFlag(TargetType.Enemy))
            {
                return GameManager.Instance.team2Entity;
            }
            if (Type.HasFlag(TargetType.Select))
            {
                return _selectTargetEntity;
            }
            if (Type.HasFlag(TargetType.Me))
            {
                return GameManager.Instance.team1Entity;
            }

            Debug.LogError(Type + " GetTarget 실패.");
            return null;
        }
    }
}