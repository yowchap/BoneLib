using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class BoolElement : Element
    {
        public BoolElement(string name, Color color, bool startValue, Action<bool> callback) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _elementType = "Function";
            _startValue = startValue;
            _callback = callback;
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
                OnElementChanged?.Invoke();
            }
        }

        private bool _startValue;
        private bool _value;
        private Action<bool> _callback;

        public override void OnElementSelected()
        {
            _value = !_value;
            _callback?.Invoke(_value);
        }
    }
}