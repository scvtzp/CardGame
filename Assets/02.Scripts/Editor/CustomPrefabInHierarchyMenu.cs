using UnityEngine;
using UnityEditor;
    
namespace Editor
{
    public class CustomPrefabInHierarchyMenu
    {
        private const string menuPath = "GameObject/Instantiate Prefabs/";
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

            // 생성된 오브젝트를 선택 상태로
            Selection.activeObject = instance;
        }

        //프리팹 생성 메뉴
        [MenuItem(menuPath + "Button", false, 0)]
        private static void InstantiateAPrefab()
        {
            InstantiatePrefabInScene($"{filePath}Button.prefab");
        }
    }

}