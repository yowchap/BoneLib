﻿using System;
using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class BoolElement : GenericElement<bool>
    {
        public BoolElement(string name, Color color, bool value, Action<bool> action = null) : base(name, color, action)
        {
            Name = name;
            Color = color;
            _value = value;
            this._action = action;
        }

        public override ElementType Type => ElementType.Toggle;
        public override string DisplayValue => _value ? "Enabled" : "Disabled";

        public bool Value => _value;

        public override void OnSelectElement()
        {
            _value = !_value;
            OnChangedValue();
        }

        protected override void OnChangedValue()
        {
            base.OnChangedValue();
        }
    }
}
