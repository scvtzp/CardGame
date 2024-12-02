using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SplashScene : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.Instance.PlayBgm("Bgm");
        }

        public void OnPressedStartButton()
        {
            SceneManager.LoadScene("InGameScene");
        }

        public void OnPressedExitButton()
        {
            Application.Quit();
        }

        public void OnPressedStatsButton()
        {
            ;
        }
    }
}