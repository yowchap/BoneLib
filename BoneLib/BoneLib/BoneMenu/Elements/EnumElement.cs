using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class EnumElement : Element
    {
        public EnumElement(string name, Color color, Enum value, Action<Enum> callback) : base(name, color)
        {
            _internalValues = Enum.GetValues(value.GetType());
            _value = value;
            _callback = callback;
            int i = 0;
            foreach (object obj in _internalValues)
            {
                if ((obj as Enum).ToString() == _value.ToString()) // There has to be a better way to do this
                {
                    _index = i + 1;
                    break;
                }
                i++;
            }
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
        private Action<Enum> _callback;
        private Enum _value;
        private Array _internalValues;
        private int _index = 1;
        private Action<Enum> _callback;

        public void GetNext()
        {
            _index %= _internalValues.Length;
            _value = _internalValues.GetValue(_index++) as Enum;
            _callback?.InvokeActionSafe(_value);
        }
    }
}