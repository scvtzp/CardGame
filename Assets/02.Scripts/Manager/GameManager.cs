using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public delegate void KillDelegate();

    public enum KillType
    {
        Ally,
        Enemy
    }
    
    public class GameManager : Singleton<GameManager>
    {
        public Dictionary<KillType, KillDelegate> KillDelegates = new();

        private void KillAction(KillType killType)
        {
            KillDelegates[killType].Invoke();
        }
        
        
    }
}

