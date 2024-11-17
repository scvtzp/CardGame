using System.Collections.Generic;
using Manager.Generics;
using UI.UIBase;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Manager
{
    public class ViewManager : Singleton<ViewManager>
    {
        private Dictionary<string, ViewBase> _views = new Dictionary<string, ViewBase>();
        private GameObject _root;

        public void ShowView<T>() where T : ViewBase 
        {
            var name = typeof(T).Name;
            
            if (_views.TryGetValue(name, out ViewBase view))
                ShowView(view);
            
            else
            {
                Addressables.LoadAssetAsync<GameObject>($"Assets/03.Prefabs/View/{name}.prefab").Completed += handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        var prefab = handle.Result;
                        
                        view = GameObject.Instantiate(prefab, GetCanvas()).GetComponent<ViewBase>();
                        _views.Add(name, view);
                        view.Init();
                        ShowView(view);
                    }
                    else
                        Debug.LogWarning($"어드레서블에서 {name}를 찾을 수 없습니다.");
                };
            }
        }

        public void HideView<T>() where T : ViewBase
        {
            var name = typeof(T).Name;
            HideView(_views[name]);
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
        
        private void ShowView(ViewBase view)
        {
            view.ShowStart();
            view.Show();
            view.ShowEnd();
        }
        
        private void HideView(ViewBase view)
        {
            view.HideStart();
            view.Hide();
            view.HideEnd();
        }
    }
}