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
            IncrementValue = increment;
            Callback = callback;
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

        public float MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                _minValue = value;
                _value = Mathf.Clamp(_value, _minValue, _maxValue);
                OnElementChanged.InvokeActionSafe();
            }
        }

        public float MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
                _value = Mathf.Clamp(_value, _minValue, _maxValue);
                OnElementChanged.InvokeActionSafe();
            }
        }

        public Action<float> Callback { get; set; }

        private float _value;
        private float _minValue;
        private float _maxValue;
        public float IncrementValue { get; set; }

        public void Increment()
        {
            _value += IncrementValue;
            _value = Mathf.Clamp(_value, _minValue, _maxValue);

            OnValueChanged.InvokeActionSafe(this, _value);
            Callback.InvokeActionSafe(_value);
        }

        public void Decrement()
        {
            _value -= IncrementValue;
            _value = Mathf.Clamp(_value, _minValue, _maxValue);

            OnValueChanged.InvokeActionSafe(this, _value);
            Callback.InvokeActionSafe(_value);
        }
    }
}