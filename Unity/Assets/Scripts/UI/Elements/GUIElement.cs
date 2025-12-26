using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    public abstract class GUIElement : MonoBehaviour
    {
        protected Button _infoButton;

        public virtual void OnHover()
        {
        }

        public virtual void OnSelected()
        {
        }

        public virtual void OnDeselected()
        {
        }

        public virtual void OnPressed()
        {
        }

        public virtual void Draw()
        {
            gameObject.SetActive(true);
        }
    }
}