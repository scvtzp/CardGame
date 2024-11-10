using CardGame.Entity;
using DefaultNamespace;
using R3;
using UnityEngine;
using Generics;

namespace Manager
{
    public class StageManager : Singleton<StageManager>
    {
        //todo: 나중에 어드래서블 로드 방식으로 수정.
        [SerializeField] private Entity player;
        [SerializeField] private Entity monster;
        
        public ReactiveProperty<int> Stage =  new(0);
        
        public void SetStage(string stageName)
        {
            player.SetDeck(DefaultDeckManager.Instance.defaultDeckSetting["Player_1"]);
            Entity clone = Instantiate(monster, new Vector3(5.17f, -1.23f, 0), Quaternion.identity);
            clone.SetDeck(DefaultDeckManager.Instance.defaultDeckSetting["Monster_1"]);
            GameManager.Instance.AddEntity(player, ObjectType.Team1);
            GameManager.Instance.AddEntity(clone, ObjectType.Team2);
        }

        private void SetDeck(Entity entity, string id)
        {
            
        }

        public void ChangeStage()
        {
            Stage.Value++;
            Debug.Log("stage: " + Stage);
            Entity clone = Instantiate(monster, new Vector3(5.17f, -1.23f, 0), Quaternion.identity);
            clone.SetDeck(DefaultDeckManager.Instance.defaultDeckSetting["Monster_1"]);
            GameManager.Instance.AddEntity(clone, ObjectType.Team2);
        }
    }
}