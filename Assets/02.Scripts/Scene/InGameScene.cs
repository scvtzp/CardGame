using System;
using Manager;
using UnityEngine;

namespace Scene
{
    public class InGameScene : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.Instance.PlayBgm("Bgm");
        }
    }
}