using System.Threading.Tasks;
using Manager;
using UnityEngine;

namespace UI.UIBase
{
    public class ViewBase : MonoBehaviour
    {
        public virtual void Init()
        {
            
        } 
        
        public async virtual Task ShowStart()
        {
            return;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void ShowEnd()
        {
            
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