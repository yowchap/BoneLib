using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public sealed class BoolElement : Element
    {
        public BoolElement(string name, Color color, bool startValue, Action<bool> callback = null) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _startValue = startValue;
            Callback = callback;
            _value = _startValue;
        }

        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnElementChanged.InvokeActionSafe();
            }
        }

        private bool _startValue;
        private bool _value;
        public Action<bool> Callback { get; set; }

        public override void OnElementSelected()
        {
            _value = !_value;
            Callback.InvokeActionSafe(_value);
        }
    }
}