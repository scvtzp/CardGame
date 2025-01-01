using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Manager.Generics;
using UI.UIBase;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Manager
{
    public class ViewManager : NonDontDestroySingleton<ViewManager>
    {
        private Dictionary<string, ViewBase> _views = new Dictionary<string, ViewBase>();
        private GameObject _root;

        public async UniTask ShowView<T>() where T : ViewBase 
        {
            await ShowView(typeof(T).Name);
        }

        public async UniTask ShowView(string viewName)
        {
            if (_views.TryGetValue(viewName, out ViewBase view))
                await ShowView(view);
            
            else
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>($"Assets/03.Prefabs/View/{viewName}.prefab");
                await handle.Task;
                
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    var prefab = handle.Result;
                        
                    view = GameObject.Instantiate(prefab, GetCanvas()).GetComponent<ViewBase>();
                    _views.Add(viewName, view);
                    view.Init();
                    await ShowView(view);
                }
                else
                    Debug.LogWarning($"어드레서블에서 {viewName}를 찾을 수 없습니다.");
            }
        }
        
        private Transform GetCanvas()
        {
            if(_root != null)
                return _root.transform;
            
            var obj = GameObject.Find("Canvas");
            if (obj == null)
            {
                Debug.LogWarning($"Canvas를 찾을 수 없습니다.");
                return null;
            }
            else
            {
                _root = obj;
                return _root.transform;
            }
        }
        
        private async Task ShowView(ViewBase view)
        {
            await view.ShowStart();
            view.Show();
            await view.ShowEnd();
        }
        
        public void HideView<T>() where T : ViewBase => HideView(typeof(T).Name);
        public void HideView(string viewName) => HideView(_views[viewName]);
        private void HideView(ViewBase view)
        {
            view.HideStart();
            view.Hide();
            view.HideEnd();
        }
    }
}