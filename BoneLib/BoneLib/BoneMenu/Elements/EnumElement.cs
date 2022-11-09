using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class EnumElement<Enum> : GenericElement<Enum>
    {
        public EnumElement(string name, Color color) : base(name, color)
        {
            Name = name;
            Color = color;
        }

        public override string DisplayValue => value.ToString();

        public override void OnSelectLeft()
        {
            value = GetNextValue();
        }

        public override void OnSelectRight()
        {
            value = GetPreviousValue();
        }

        // from MTINM.BoneMenu
        private Enum GetNextValue()
        {
            Array values = System.Enum.GetValues(value.GetType());
            int nextIndex = Array.IndexOf(values, value) + 1;
            Debug.Log(nextIndex);
            return nextIndex == values.Length ? (Enum)values.GetValue(0) : (Enum)values.GetValue(nextIndex);
        }

        private Enum GetPreviousValue()
        {
            Array values = System.Enum.GetValues(value.GetType());
            int previousIndex = Array.IndexOf(values, value) - 1;
            Debug.Log(previousIndex);
            return previousIndex > -1 ? (Enum)values.GetValue(previousIndex) : (Enum)values.GetValue(values.Length - 1);
        }
    }
}
    