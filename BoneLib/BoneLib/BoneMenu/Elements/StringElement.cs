using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [System.Serializable]
    public class StringElement : Element
    {
        public StringElement(string name, Color color, string startValue, Action<string> callback) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _startValue = startValue;
            _callback = callback;
        }
        
        public string Value => _value;

        private string _startValue;
        private string _value;
        private Action<string> _callback;

        public override void OnElementSelected()
        {
            base.OnElementSelected();
            _callback?.Invoke(_value);
        }

        public void SetText(string text)
        {
            _value = text;
        }
    }
}