using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public abstract class GUIElement : MonoBehaviour
    {   
        public GUIElement(System.IntPtr ptr) : base(ptr) { }

        protected Button _infoButton;
        protected GUIInfoBox _infoBox;

        public virtual void OnHover(){}
        public virtual void OnSelected(){}
        public virtual void OnDeselected(){}
        public virtual void OnPressed(){}

        public virtual void Draw()
        {
            gameObject.SetActive(true);
        }
    }
}