using System;
using System.Collections.Generic;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public class MenuCategory : MenuElement
    {
        public MenuCategory(string Name, Color Color) : base(Name, Color) { }

        public override string Type => ElementType.Type_Category;

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

        public FunctionElement CreateFunctionElement(string name, Color color, Action action)
        {
            var element = new FunctionElement(name, color, action);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        public IntElement CreateIntElement(string name, Color color, int startValue, int increment, int minValue, int maxValue)
        {
            var element = new IntElement(name, color, startValue, increment, minValue, maxValue);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        public EnumElement<T> CreateEnumElement<T>(string name, Color color) where T : Enum
        {
            var element = new EnumElement<T>(name, color);
            Elements?.Add(element);
            OnElementCreated?.Invoke(this, element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        public override void OnSelectElement()
        {
            SafeActions.InvokeActionSafe(OnElementSelected, this, this);
        }
    }
}
