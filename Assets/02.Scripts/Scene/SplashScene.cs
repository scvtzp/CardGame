using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SplashScene : MonoBehaviour
    {
        public void OnPressedStartButton()
        {
            SceneManager.LoadScene("InGameScene");
        }

        public void OnPressedExitButton()
        {
            Application.Quit();
        }
    }
}