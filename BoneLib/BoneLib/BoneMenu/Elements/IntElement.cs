using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class IntElement : Element
    {
        public IntElement(string name, Color color, int increment, int startValue, int minValue, int maxValue, Action<int> callback) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _elementType = "Int";

            _value = startValue;
            _minValue = minValue;
            _maxValue = maxValue;
            _increment = increment;
        }

        public static Action<Element, int> OnValueChanged;

        public int Value => _value;

        private int _value;
        private int _minValue;
        private int _maxValue;
        private int _increment;

        public void Increment()
        {
            // Clamped value between minValue and maxValue
            _value = Mathf.Min(_maxValue, Mathf.Max(_minValue, _value + _increment));
            OnValueChanged?.Invoke(this, _value);
        }

        public void Decrement()
        {
            // Clamped value between minValue and maxValue
            _value = Mathf.Max(_minValue, Mathf.Min(_maxValue, _value - _increment));
            OnValueChanged?.Invoke(this, _value);
        }
    }
}