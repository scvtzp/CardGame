using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Manager;
using UnityEngine;

namespace UI.UIBase
{
    public class ViewBase : MonoBehaviour
    {
        public virtual void Init()
        {
            
        } 
        
        public async virtual UniTask ShowStart()
        {
            return;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        
        public async virtual UniTask ShowEnd()
        {
            return;
        }

        public virtual void HideStart()
        {
            
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void HideEnd()
        {
            
        }

        public void OnPressedBackKey()
        {
            ViewManager.Instance.HideView(GetType().Name);
        }
    }
}