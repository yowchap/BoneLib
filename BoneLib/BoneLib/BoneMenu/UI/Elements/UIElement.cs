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

        public virtual ElementType Type => ElementType.Default;

        public TextMeshPro NameText => GetTextMesh("Name");
        public TextMeshPro ValueText => GetTextMesh("Value");

        protected MenuElement element;

        [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
        public void AssignElement(MenuElement element)
        {
            this.element = element;

            if (NameText != null)
            {
                NameText.text = this.element.Name;
                NameText.color = this.element.Color;
            }

            if (ValueText != null)
            {
                ValueText.text = this.element.DisplayValue;
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
