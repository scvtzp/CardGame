using UnityEngine;

namespace UI.UIBase
{
    public class ViewBase : MonoBehaviour
    {
        public virtual void Init()
        {
            
        } 
        
        public virtual void ShowStart()
        {
            
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

    }
}