using System;

using UnityEngine;

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

        public virtual ElementType Type => ElementType.Default;
        public virtual string DisplayValue => "Default";

        internal Action OnUpdateVisuals;

        public void SetName(string name)
        {
            Name = name;
            OnUpdateVisuals.InvokeActionSafe();
        }

        public void SetColor(Color color)
        {
            Color = color;
            OnUpdateVisuals.InvokeActionSafe();
        }

        public virtual void OnSelectElement() { }
        public virtual void OnSelectLeft() { }
        public virtual void OnSelectRight() { }
    }
}
