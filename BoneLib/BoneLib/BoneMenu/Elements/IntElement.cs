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
            _value += _increment;

            if (_value >= _maxValue)
            {
                _value = _maxValue;
            }
        }

        public override void OnSelectRight()
        {
            _value -= _increment;

            if (_value <= _minValue)
            {
                _value = _minValue;
            }
        }

        protected override void OnValueChanged() { }
    }
}
