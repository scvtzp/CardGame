using UnityEngine;
using UnityEditor;
    
namespace Editor
{
    public class CustomPrefabInHierarchyMenu
    {
        private const string menuPath = "GameObject/UI 프리팹/";
        private const string filePath = "Assets/03.Prefabs/Default/";

        // 공통 프리팹 생성 함수, 선택된 부모 오브젝트에 따라 위치를 설정
        private static void InstantiatePrefabInScene(string prefabPath)
        {
            // 프리팹 로드
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab == null)
            {
                Debug.LogError($"생성하려는 프리팹이 존재하지 않습니다: {prefabPath}"); 
                return;
            }

            // 선택된 오브젝트를 부모로 설정
            GameObject parent = Selection.activeGameObject;

            // 프리팹 인스턴스 생성
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(instance, "Instantiate " + instance.name);

            // 부모가 있다면 자식으로, 없으면 루트에 배치
            if (parent != null)
            {
                instance.transform.SetParent(parent.transform);
                instance.transform.localPosition = Vector3.zero;
            }
            else
            {
                // 씬의 중앙에 배치
                if (SceneView.lastActiveSceneView != null)
                    SceneView.lastActiveSceneView.MoveToView(instance.transform);
                else
                    instance.transform.position = Vector3.zero;
            }
            
            //캔버스 스케일이 0.001이러니까 생성될때 월드기준 1 맞추려고 localScale이 100돼서 무조건 1로 고정.
            instance.transform.localScale = Vector3.one;

            // 생성된 오브젝트를 선택 상태
            Selection.activeObject = instance;
        }

        //프리팹 생성 메뉴
        [MenuItem(menuPath + "Button", false, 0)]
        private static void InstantiateAPrefab() => InstantiatePrefabInScene($"{filePath}Button.prefab");
        
        [MenuItem(menuPath + "Dimming", false, 0)]
        private static void InstantiateDimmingPrefab() => InstantiatePrefabInScene($"{filePath}Dimming.prefab");
        
        [MenuItem(menuPath + "LoopScrollView", false, 0)]
        private static void InstantiatePrefab_LoopScrollView() => InstantiatePrefabInScene($"{filePath}LoopScrollView.prefab");
        
        [MenuItem(menuPath + "TextBold", false, 0)]
        private static void InstantiatePrefab_TextBold() => InstantiatePrefabInScene($"{filePath}TextBold.prefab");
    }

}