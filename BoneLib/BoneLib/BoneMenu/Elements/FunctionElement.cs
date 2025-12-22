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
            Callback = callback;
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
        public Action Callback { get; set; }

        public override void OnElementSelected()
        {
            Callback.InvokeActionSafe();
        }
    }
}