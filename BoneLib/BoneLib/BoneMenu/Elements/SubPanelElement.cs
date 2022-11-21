using System;
using System.Collections.Generic;

using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class SubPanelElement : MenuElement
    {
        public SubPanelElement(string Name, Color Color) : base(Name, Color) { }

        public override ElementType Type => ElementType.SubPanel;

        public List<MenuElement> Elements = new List<MenuElement>();

        public FunctionElement CreateFunctionElement(string name, Color color, Action action)
        {
            var element = new FunctionElement(name, color, action);
            Elements?.Add(element);
            return element;
        }

        public FunctionElement CreateFunctionElement(string name, Color color, Action action, string confirmText = "")
        {
            var element = new FunctionElement(name, color, action, confirmText);
            Elements?.Add(element);
            return element;
        }

        public IntElement CreateIntElement(string name, Color color, int startValue, int increment, int minValue, int maxValue, Action<int> action = null)
        {
            var element = new IntElement(name, color, startValue, increment, minValue, maxValue, action);
            Elements?.Add(element);
            return element;
        }

        public BoolElement CreateBoolElement(string name, Color color, bool value)
        {
            var element = new BoolElement(name, color, value);
            Elements?.Add(element);
            return element;
        }

        public EnumElement<T> CreateEnumElement<T>(string name, Color color) where T : Enum
        {
            var element = new EnumElement<T>(name, color);
            Elements?.Add(element);
            return element;
        }
    }
}
