using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.UIBase
{
    public class ButtonCell : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        public void SetButton(UnityAction action) => button.onClick.AddListener(action);
        public void ReSetButton(UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }
    }
}