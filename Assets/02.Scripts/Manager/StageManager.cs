using System;
using CardGame.Entity;
using DefaultNamespace;
using UnityEngine;

namespace Manager
{
    public class StageManager : Singleton<StageManager>
    {
        //todo: 나중에 어드래서블 로드 방식으로 수정.
        [SerializeField] private Entity player;
        [SerializeField] private Entity monster;
        
        public void SetStage(string stageName)
        {
            player.SetDeck(DefaultDeckManager.Instance.defaultDeckSetting["Player_1"]);
            Instantiate(monster, new Vector3(5.17f, -1.23f, 0), Quaternion.identity).SetDeck(DefaultDeckManager.Instance.defaultDeckSetting["Monster_1"]);
            GameManager.Instance.AddEntity(player, ObjectType.Team1);
            GameManager.Instance.AddEntity(monster, ObjectType.Team2);
        }

        private void SetDeck(Entity entity, string id)
        {
            
        }
    }
}