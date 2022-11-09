using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPoolee : MonoBehaviour
    {
        public UIPoolee(System.IntPtr ptr) : base(ptr) { }

        public UIPool parent { get; private set; }

        public void SetParent(UIPool parent)
        {
            this.parent = parent;
        }

        public void Return()
        {
            parent.Active.Remove(this);
            transform.SetParent(parent.transform);
            gameObject.SetActive(false);
            parent.Inactive.Add(this);
        }
    }
}

