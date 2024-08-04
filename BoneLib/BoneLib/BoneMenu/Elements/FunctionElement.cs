using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class FunctionElement : Element
    {
        public FunctionElement(string name, Color color, Action callback) : base(name, color)
        {
            _elementName = name;
            _elementColor = color;
            _callback = callback;
        }

        public Texture2D Logo 
        {
            get
            {
                return _logo;
            }
            set
            {
                _logo = value;
                OnElementChanged.InvokeActionSafe();
            }
        }

        private Texture2D _logo;
        private Action _callback;

        public override void OnElementSelected()
        {
            _callback.InvokeActionSafe();
        }
    }
}