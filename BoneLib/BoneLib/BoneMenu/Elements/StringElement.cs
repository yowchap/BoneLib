using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public sealed class StringElement : Element
    {
        public StringElement(string name, Color color, string startValue = "", Action<string> callback = null) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _startValue = startValue;
            _value = _startValue;
            Callback = callback;
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
        public Action<string> Callback { get; set; }

        public override void OnElementSelected()
        {
            base.OnElementSelected();
            Callback.InvokeActionSafe(_value);
        }
    }
}