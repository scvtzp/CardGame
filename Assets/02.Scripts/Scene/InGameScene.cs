using System;
using DefaultNamespace;
using Manager;
using UnityEngine;

namespace Scene
{
    public class InGameScene : MonoBehaviour
    {
        private void Start()
        {
            DefaultDeckManager.Instance.Init();
            SoundManager.Instance.PlayBgm("Bgm");
            StageManager.Instance.SetStage("");
        }
    }
}