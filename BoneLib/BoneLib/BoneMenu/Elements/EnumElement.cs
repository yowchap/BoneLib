using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class EnumElement : Element
    {
        public EnumElement(string name, Color color, Enum value) : base(name, color)
        {
            _internalValues = Enum.GetValues(value.GetType());
            _value = _internalValues.GetValue(0) as Enum;
        }

        public Enum Value => _value;

        private Enum _value;
        private Array _internalValues;
        private int _index = 1;

        public void GetNext()
        {
            _index %= _internalValues.Length;
            _value = _internalValues.GetValue(_index++) as Enum;
        }
    }
}