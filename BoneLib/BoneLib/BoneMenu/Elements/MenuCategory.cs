using System;
using System.Collections.Generic;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class MenuCategory : MenuElement
    {
        public MenuCategory(string Name, Color Color) : base(Name, Color) { }

        public override string Type => ElementType.Category;

        public static Action<MenuCategory, MenuElement> OnElementCreated;
        public static Action<MenuCategory, MenuElement> OnElementSelected;

        public List<MenuElement> Elements { get; private set; } = new List<MenuElement>();

        public MenuCategory Parent { get => _parent; }
        private MenuCategory _parent;

        public void SetParent(MenuCategory parent)
        {
            _parent = parent;
        }

        public MenuCategory CreateCategory(string name, Color color)
        {
            var category = new MenuCategory(name, color);
            category.SetParent(this);
            Elements?.Add(category);
            SafeActions.InvokeActionSafe(OnElementCreated, this, category);
            return category;
        }

        public MenuCategory CreateCategory(string name, string hexColor)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateCategory(name, color);
        }

        public FunctionElement CreateFunctionElement(string name, Color color, Action action)
        {
            var element = new FunctionElement(name, color, action);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        public FunctionElement CreateFunctionElement(string name, string hexColor, Action action)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateFunctionElement(name, color, action);
        }

        public BoolElement CreateBoolElement(string name, Color color, bool value, Action<bool> action = null)
        {
            var element = new BoolElement(name, color, value, action);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            return element;
        }

        public BoolElement CreateBoolElement(string name, string hexColor, bool value, Action<bool> action = null)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateBoolElement(name, color, value, action);
        }

        public IntElement CreateIntElement(string name, Color color, int startValue, int increment, int minValue, int maxValue, Action<int> action = null)
        {
            var element = new IntElement(name, color, startValue, increment, minValue, maxValue, action);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        public IntElement CreateIntElement(string name, string hexColor, int startValue, int increment, int minValue, int maxValue, Action<int> action = null)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateIntElement(name, color, startValue, increment, minValue, maxValue, action);
        }

        public EnumElement<T> CreateEnumElement<T>(string name, Color color, Action<T> action = null) where T : Enum
        {
            var element = new EnumElement<T>(name, color, action);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        public EnumElement<T> CreateEnumElement<T>(string name, string hexColor, Action<T> action = null) where T : Enum
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateEnumElement<T>(name, color, action);
        }

        public FloatElement CreateFloatElement(string name, Color color, float startValue, float increment, float minValue, float maxValue, Action<float> action = null)
        {
            var element = new FloatElement(name, color, startValue, increment, minValue, maxValue, action);
            Elements?.Add(element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        public FloatElement CreateFloatElement(string name, string hexColor, float startValue, float increment, float minValue, float maxValue, Action<float> action = null)
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateFloatElement(name, color, startValue, increment, minValue, maxValue, action);
        }

        public ListElement<T> CreateListElement<T>(string name, Color color) where T : class
        {
            var element = new ListElement<T>(name, color);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            return element;
        }

        public ListElement<T> CreateListElement<T>(string name, string hexColor) where T : class
        {
            Color32 color;
            ColorUtility.DoTryParseHtmlColor(hexColor, out color);
            return CreateListElement<T>(name, color);
        }

        public override void OnSelectElement()
        {
            SafeActions.InvokeActionSafe(OnElementSelected, this, this);
        }
    }
}
