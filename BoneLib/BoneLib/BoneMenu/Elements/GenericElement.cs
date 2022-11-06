using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public abstract class GenericElement<T> : MenuElement
    {
        public GenericElement(string name, Color color) : base(name, color)
        {
            Name = name;
            Color = color;
        }

        public override string Type => ElementType.Type_Value;

        protected T value;
        protected Action<T> action;

        protected virtual void OnValueChanged() { }
    }
}
