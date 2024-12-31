using DG.DemiEditor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Editor
{
    [InitializeOnLoad]
    public static class PlayFromSplashButton
    {
        static PlayFromSplashButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnLeftToolbarGUI);
            ToolbarExtender.RightToolbarGUI.Add(OnRightToolbarGUI);
        }

        private static void OnLeftToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(new GUIContent("Splash", "SplashScene으로 이동합니다"), EditorStyles.miniButtonLeft))
            {
                ChangeScene("SplashScene");
            }

            GUILayout.Space(10);
            
            if (GUILayout.Button(new GUIContent("InGameScene", "InGameScene으로 이동합니다"), EditorStyles.miniButtonLeft))
            {
                ChangeScene("InGameScene");
            }
        }
        
        private static void OnRightToolbarGUI()
        {
            // 이거 키면 오른쪽으로 버튼이 붙음. 이유 몰루 EditerStyles 바꿔봐도 그대로. 주석처리하면 되길래 그냥 주석처리.
            //GUILayout.FlexibleSpace();
            
            GUILayout.Space(3);
            if (GUILayout.Button(new GUIContent("Play", "스플래시 씬으로 변경 후 실행합니다."), GUILayout.ExpandWidth(false)))
            {
                ChangeScene("SplashScene");
                
                // 플레이 모드 시작
                EditorApplication.isPlaying = true;
            }
        }

        private static void ChangeScene(string sceneName)
        {
            // 현재 씬 저장 여부 확인
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // 씬 열기
                EditorSceneManager.OpenScene($"Assets/01.Scenes/{sceneName}.unity");
            }
        }
    }

}