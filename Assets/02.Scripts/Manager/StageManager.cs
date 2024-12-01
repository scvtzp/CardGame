using CardGame.Entity;
using DefaultNamespace;
using R3;
using UnityEngine;
using Manager.Generics;
using UI.StageReward;

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
            GameManager.Instance.AddEntity(player, TargetType.Ally);
            GameManager.Instance.playerDeck = player.deckService;

            StartNewStage();
        }

        private void SetDeck(Entity entity, string id)
        {
            
        }

        //todo: 다음 스테이지 시작 애니메이션 + 리워드랑 동시에 안되게 딜레이 추가.
        public void ChangeStage()
        {
            Stage.Value++;
            Debug.Log("stage: " + Stage);
            ViewManager.Instance.ShowView<StageRewardView>();
            StartNewStage();
        }

        private void StartNewStage()
        {
            Entity clone = Instantiate(monster, new Vector3(5.17f, -1.23f, 0), Quaternion.identity);
            clone.SetDeck(DefaultDeckManager.Instance.defaultDeckSetting["Monster_1"]);
            GameManager.Instance.AddEntity(clone, TargetType.Enemy);
        }
    }
}