using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public sealed class IntElement : Element
    {
        public IntElement(string name, Color color, int startValue, int increment, int minValue, int maxValue, Action<int> callback) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;

            _value = startValue;
            _minValue = minValue;
            _maxValue = maxValue;
            IncrementValue = increment;
            Callback = callback;
        }

        public static event Action<Element, int> OnValueChanged;

        public int Value
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

        public int MinValue
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

        public int MaxValue
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

        private int _value;
        private int _minValue;
        private int _maxValue;
        public int IncrementValue { get; set; }

        public Action<int> Callback { get; set; }

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