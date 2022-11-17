using System;

using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class EnumElement<Enum> : GenericElement<Enum>
    {
        public EnumElement(string name, Color color, Action<Enum> action = null) : base(name, color, action)
        {
            Name = name;
            Color = color;
            this._action = action;
        }

        public override string Type => ElementType.Value;
        public override string DisplayValue => _value.ToString();

        public override void OnSelectLeft()
        {
            _value = GetNextValue();
            OnChangedValue();
        }

        public override void OnSelectRight()
        {
            _value = GetPreviousValue();
            OnChangedValue();
        }

        // from MTINM.BoneMenu
        private Enum GetNextValue()
        {
            Array values = System.Enum.GetValues(_value.GetType());
            int nextIndex = Array.IndexOf(values, _value) + 1;
            return nextIndex == values.Length ? (Enum)values.GetValue(0) : (Enum)values.GetValue(nextIndex);
        }

        private Enum GetPreviousValue()
        {
            Array values = System.Enum.GetValues(_value.GetType());
            int previousIndex = Array.IndexOf(values, _value) - 1;
            return previousIndex > -1 ? (Enum)values.GetValue(previousIndex) : (Enum)values.GetValue(values.Length - 1);
        }

        protected override void OnChangedValue()
        {
            base.OnChangedValue();
        }
    }
}
    