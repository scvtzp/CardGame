using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
    
namespace Editor
{
    [InitializeOnLoad]
    public static class PlayFromSplashButton
    {
        static PlayFromSplashButton()
        {
            // 에디터 상단의 툴바를 렌더링하는 콜백 등록
            EditorApplication.update += AddToolbarButton;
        }

        private static void AddToolbarButton()
        {
            // 툴바에 GUI를 그리기 위해 반복 실행
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            Handles.BeginGUI();

            // 툴바 버튼의 위치 정의
            Rect buttonRect = new Rect(10, 10, 100, 30);

            // 버튼 생성
            if (GUI.Button(buttonRect, "Play Splash"))
            {
                PlayFromSplash();
            }

            Handles.EndGUI();
        }

        private static void PlayFromSplash()
        {
            string sceneName = "Splash";

            // 현재 씬 저장 여부 확인
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // 씬 열기
                EditorSceneManager.OpenScene($"Assets/01.Scenes/{sceneName}.unity");

                // 플레이 모드 시작
                EditorApplication.isPlaying = true;
            }
        }
    }

}