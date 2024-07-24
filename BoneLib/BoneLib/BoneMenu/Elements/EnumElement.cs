using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class EnumElement : Element
    {
        public EnumElement(string name, Color color, Enum value, Action<Enum> callback = null) : base(name, color)
        {
            _internalValues = Enum.GetValues(value.GetType());
            _value = _internalValues.GetValue(0) as Enum;
            _callback = callback;
        }

        public Enum Value
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

        private Enum _value;
        private Array _internalValues;
        private int _index = 1;
        private Action<Enum> _callback;

        public void GetNext()
        {
            _index %= _internalValues.Length;
            _value = _internalValues.GetValue(_index++) as Enum;
            _callback?.Invoke(_value);
        }
    }
}