using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public sealed class FloatElement : Element
    {
        public FloatElement(string name, Color color, float startValue, float increment, float minValue, float maxValue, Action<float> callback = null) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;

            _value = startValue;
            _minValue = minValue;
            _maxValue = maxValue;
            _increment = increment;
            _callback = callback;
        }

        public static event Action<Element, float> OnValueChanged;

        public float Value
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

        private Action<float> _callback;

        private float _value;
        private float _minValue;
        private float _maxValue;
        private float _increment;

        public void Increment()
        {
            _value += _increment;
            _value = Mathf.Clamp(_value, _minValue, _maxValue);

            OnValueChanged.InvokeActionSafe(this, _value);
            _callback.InvokeActionSafe(_value);
        }

        public void Decrement()
        {
            _value -= _increment;
            _value = Mathf.Clamp(_value, _minValue, _maxValue);

            OnValueChanged.InvokeActionSafe(this, _value);
            _callback.InvokeActionSafe(_value);
        }
    }
}