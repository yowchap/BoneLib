using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class IntElement : Element
    {
        public IntElement(string name, Color color, int startValue, int increment, int minValue, int maxValue, Action<int> callback) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _elementType = "Int";

            _value = startValue;
            _minValue = minValue;
            _maxValue = maxValue;
            _increment = increment;
            _callback = callback;
        }
        public static Action<Element, int> OnValueChanged;

        public int Value
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

        private int _value;
        private int _minValue;
        private int _maxValue;
        private int _increment;

        private Action<int> _callback;

        public void Increment()
        {
            // Clamped value between minValue and maxValue
            _value = Mathf.Min(_maxValue, Mathf.Max(_minValue, _value + _increment));
            OnValueChanged?.Invoke(this, _value);
            _callback?.InvokeActionSafe(_value);
        }

        public void Decrement()
        {
            // Clamped value between minValue and maxValue
            _value = Mathf.Max(_minValue, Mathf.Min(_maxValue, _value - _increment));
            OnValueChanged?.Invoke(this, _value);
            _callback?.InvokeActionSafe(_value);
        }
    }
}