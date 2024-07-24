using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class FloatElement : Element
    {
        public FloatElement(string name, Color color, float startValue, float increment, float minValue, float maxValue, Action<float> callback = null) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _elementType = "Float";

            _value = startValue;
            _minValue = minValue;
            _maxValue = maxValue;
            _increment = increment;
            _callback = callback;
        }
        private Action<float> _callback;
        public static Action<Element, float> OnValueChanged;

        public float Value
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

        private float _value;
        private float _minValue;
        private float _maxValue;
        private float _increment;

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