using UnityEngine;

using TMPro;

namespace BoneLib.BoneMenu.Elements
{
    public abstract class MenuElement
    {
        public MenuElement(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; protected set; }
        public Color Color { get; protected set; }

        public virtual ElementType Type { get => ElementType.Default; }
        public virtual string DisplayValue { get => "Default"; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }

        public virtual void OnSelectElement() { }
        public virtual void OnSelectLeft() { }
        public virtual void OnSelectRight() { }
    }
}
