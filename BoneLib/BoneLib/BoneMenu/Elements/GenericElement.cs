using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public abstract class GenericElement<T> : MenuElement
    {
        public GenericElement(string name, Color color, Action<T> onChangedAction = null) : base(name, color)
        {
            Name = name;
            Color = color;
            action = onChangedAction;
        }

        public override string Type => ElementType.Type_Value;

        protected T value;
        protected Action<T> action;

        protected virtual void OnChangedValue()
        {
            SafeActions.InvokeActionSafe(action, value);
        }
    }
}
