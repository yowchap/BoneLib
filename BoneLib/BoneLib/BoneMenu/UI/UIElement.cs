using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIElement : MonoBehaviour
    {
        public UIElement(IntPtr ptr) : base(ptr) { }

        public virtual string Type { get => ElementType.Default; }

        protected MenuElement _element;

        protected virtual TextMeshPro _nameText { get => GetTextMesh("Name"); }
        protected virtual TextMeshPro _valueText { get => GetTextMesh("Value"); }

        public void AssignElement(MenuElement element)
        {
            _element = element;

            if (_nameText != null)
            {
                _nameText.text = _element.Name;
                _nameText.color = _element.Color;
            }

            if (_valueText != null)
            {
                _valueText.text = _element.DisplayValue;
            }
        }

        public TextMeshPro GetTextMesh(string path)
        {
            if(transform.Find(path) == null)
            {
                return null;
            }

            return transform.Find(path).GetComponent<TextMeshPro>();
        }

        public void SetText(string name = "", string value = "")
        {
            if (_nameText == null)
            {
                return;
            }

            _nameText.text = name;

            if (_valueText == null)
            {
                return;
            }

            _valueText.text = value;
        }
    }
}
