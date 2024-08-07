using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    public sealed class StringElement : Element
    {
        public StringElement(string name, Color color, string startValue, Action<string> callback = null) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _startValue = startValue;
            _callback = callback;
        }
        
        public string Value
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

        private string _startValue;
        private string _value;
        private Action<string> _callback;

        public override void OnElementSelected()
        {
            base.OnElementSelected();
            _callback.InvokeActionSafe(_value);
        }
    }
}