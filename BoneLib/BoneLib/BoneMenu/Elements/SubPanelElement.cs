using System;
using System.Collections.Generic;

using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class SubPanelElement : MenuElement
    {
        public SubPanelElement(string Name, Color Color, Action action = null) : base(Name, Color)
        {
            onSelectAction = action;
        }

        public override ElementType Type => ElementType.SubPanel;

        public List<MenuElement> Elements = new List<MenuElement>();

        private Action onSelectAction;

        public override void OnSelectElement()
        {
            SafeActions.InvokeActionSafe(onSelectAction);
        }

        public FunctionElement CreateFunctionElement(string name, Color color, Action action)
        {
            var element = new FunctionElement(name, color, action);
            Elements?.Add(element);
            return element;
        }

        public FunctionElement CreateFunctionElement(string name, string hexColor, Action action)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateFunctionElement(name, color, action);
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

        public IntElement CreateIntElement(string name, string hexColor, int startValue, int increment, int minValue, int maxValue, Action<int> action = null)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateIntElement(name, color, startValue, increment, minValue, maxValue, action);
        }

        public FloatElement CreateFloatElement(string name, Color color, float startValue, float increment, float minValue, float maxValue, Action<float> action = null)
        {
            var element = new FloatElement(name, color, startValue, increment, minValue, maxValue, action);
            Elements?.Add(element);
            return element;
        }

        public FloatElement CreateFloatElement(string name, string hexColor, float startValue, float increment, float minValue, float maxValue, Action<float> action = null)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateFloatElement(name, color, startValue, increment, minValue, maxValue, action);
        }

        public BoolElement CreateBoolElement(string name, Color color, bool value, Action<bool> action = null)
        {
            var element = new BoolElement(name, color, value, action);
            Elements?.Add(element);
            return element;
        }

        public BoolElement CreateBoolElement(string name, string hexColor, bool value, Action<bool> action = null)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateBoolElement(name, color, value, action);
        }

        public EnumElement<T> CreateEnumElement<T>(string name, Color color, Action<T> action = null) where T : Enum
        {
            var element = new EnumElement<T>(name, color, action);
            Elements?.Add(element);
            return element;
        }

        public EnumElement<T> CreateEnumElement<T>(string name, string hexColor, Action<T> action = null) where T : Enum
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateEnumElement<T>(name, color, action);
        }
    }
}
