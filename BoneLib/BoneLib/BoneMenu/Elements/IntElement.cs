using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class IntElement : GenericElement<int>
    {
        public IntElement(string name, Color color, int startValue, int increment, int minValue, int maxValue) : base(name, color)
        {
            Name = name;
            Color = color;
            _value = startValue;
            _increment = increment;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public int Value { get => _value; }
        public int Increment { get => _increment; }
        public int MinValue { get => _minValue; }
        public int MaxValue { get => _maxValue; }

        private int _value;
        private int _increment;
        private int _minValue;
        private int _maxValue;

        public override void OnSelectLeft()
        {
            int value = _value += _increment;

            if (value >= _maxValue)
            {
                value = _maxValue;
            }

            UpdateText(_valueText, value);
        }

        public override void OnSelectRight()
        {
            int value = _value -= _increment;

            if (value <= _minValue)
            {
                value = _minValue;
            }

            UpdateText(_valueText, value);
        }

        protected override void OnValueChanged() { }
    }
}
