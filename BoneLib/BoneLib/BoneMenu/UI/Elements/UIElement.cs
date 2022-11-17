using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using BoneLib.BoneMenu.Elements;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIElement : MonoBehaviour
    {
        public UIElement(IntPtr ptr) : base(ptr) { }

        public virtual ElementType Type { get => ElementType.Default; }

        public TextMeshPro NameText { get; private set; }
        public TextMeshPro ValueText { get; private set; }

        protected MenuElement _element;

        private void Awake()
        {
            NameText = GetTextMesh("Name");
            ValueText = GetTextMesh("Value");
        }

        public void AssignElement(MenuElement element)
        {
            _element = element;

            if (NameText != null)
            {
                NameText.text = _element.Name;
                NameText.color = _element.Color;
            }

            if (ValueText != null)
            {
                ValueText.text = _element.DisplayValue;
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
            if (NameText == null)
            {
                return;
            }

            NameText.text = name;

            if (ValueText == null)
            {
                return;
            }

            ValueText.text = value;
        }
    }
}
