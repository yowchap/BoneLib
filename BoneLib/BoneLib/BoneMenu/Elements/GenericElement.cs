using System;

using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public abstract class GenericElement<T> : MenuElement
    {
        public GenericElement(string name, Color color, Action<T> action = null) : base(name, color)
        {
            Name = name;
            Color = color;
            _action = action;
        }

        public override ElementType Type => ElementType.Value;

        protected T _value;
        protected Action<T> _action;

        public T GetValue()
        {
            return _value;
        }

        protected virtual void OnChangedValue()
        {
            SafeActions.InvokeActionSafe(_action, _value);
        }
    }
}