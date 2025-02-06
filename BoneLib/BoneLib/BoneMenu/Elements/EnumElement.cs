using System;
using System.Linq;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public sealed class EnumElement : Element
    {
        public EnumElement(string name, Color color, Enum value, Action<Enum> callback) : base(name, color)
        {
            _internalValues = Enum.GetValues(value.GetType());
            _value = value;
            Callback = callback;
            var vals = Enum.GetValues(value.GetType());
            _index = Array.IndexOf(vals, vals.OfType<Enum>().First(v => v.Equals(value))) + 1;
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
                OnElementChanged.InvokeActionSafe();
            }
        }

        public Action<Enum> Callback { get; set; }
        private Enum _value;
        private Array _internalValues;
        private int _index = 1;

        public void GetNext()
        {
            _index %= _internalValues.Length;
            _value = _internalValues.GetValue(_index++) as Enum;
            Callback.InvokeActionSafe(_value);
        }
    }
}