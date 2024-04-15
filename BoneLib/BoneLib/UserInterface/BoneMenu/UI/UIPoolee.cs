using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPoolee : MonoBehaviour
    {
        public UIPoolee(System.IntPtr ptr) : base(ptr) { }

        public UIPool parent { get; private set; }

        private Vector3 _localScale;

        private void Awake()
        {
            _localScale = transform.localScale;
        }

        private void OnEnable()
        {
            transform.localScale = _localScale;
        }

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

