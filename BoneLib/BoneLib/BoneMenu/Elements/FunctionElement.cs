using System;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class FunctionElement : MenuElement
    {
        public FunctionElement(string name, Color color, Action action) : base(name, color)
        {
            Name = name;
            Color = color;
            Action = action;
        }

        public Action Action { get; private set; }

        public override void OnSelectElement()
        {
            throw new NotImplementedException();
        }
    }
}
