using System;
using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class FloatElement : GenericElement<float>
    {
        public FloatElement(string name, Color color, float startValue, float increment, float minValue, float maxValue, Action<float> action = null) : base(name, color, action)
        {
            Name = name;
            Color = color;
            _value = startValue;
            _increment = increment;
            _minValue = minValue;
            _maxValue = maxValue;
            this._action = action;
        }

        public override ElementType Type => ElementType.Value;
        public override string DisplayValue => _value.ToString("0.####");

        public float Value { get => _value; }
        public float Increment { get => _increment; }
        public float MinValue { get => _minValue; }
        public float MaxValue { get => _maxValue; }

        private float _increment;
        private float _minValue;
        private float _maxValue;

        public override void OnSelectLeft()
        {
            _value += _increment;

            if (_value >= _maxValue)
            {
                _value = _maxValue;
            }

            OnChangedValue();
        }

        public override void OnSelectRight()
        {
            _value -= _increment;

            if (_value <= _minValue)
            {
                _value = _minValue;
            }

            OnChangedValue();
        }

        protected override void OnChangedValue()
        {
            base.OnChangedValue();
        }
    }
}
