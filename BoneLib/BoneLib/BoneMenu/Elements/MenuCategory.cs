using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class MenuCategory : MenuElement
    {
        public MenuCategory(string Name, Color Color) : base(Name, Color) { }

        public override ElementType Type => ElementType.Category;

        public static Action<MenuCategory, MenuElement> OnElementCreated;
        public static Action<MenuCategory, MenuElement> OnElementSelected;

        public List<MenuElement> Elements { get; private set; } = new List<MenuElement>();

        public MenuCategory Parent { get => _parent; }
        private MenuCategory _parent;

        public void SetParent(MenuCategory parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// Creates a category with a name and a color. 
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="color">The name's color.</param>
        /// <returns>A new category.</returns>
        public MenuCategory CreateCategory(string name, Color color)
        {
            if (Elements.Exists((item) => item.Name == name))
            {
                return Elements.Find((item) => item.Name == name) as MenuCategory;
            }

            MenuCategory category = new MenuCategory(name, color);
            category.SetParent(this);
            Elements?.Add(category);
            SafeActions.InvokeActionSafe(OnElementCreated, this, category);
            return category;
        }

        /// <summary>
        /// Creates a category with a name and a hex color input.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="hexColor">The color in hexadecimal.
        /// <code>"Example: "#00CA11" for green."</code></param>
        /// <returns>A new category with a hex color.</returns>
        public MenuCategory CreateCategory(string name, string hexColor)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color);
            return CreateCategory(name, color);
        }

        /// <summary>
        /// Creates a sub-panel with a name and a color. 
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="color">The name's color.</param>
        /// <returns>A new sub-panel in your page.</returns>
        public SubPanelElement CreateSubPanel(string name, Color color)
        {
            SubPanelElement panel = new SubPanelElement(name, color);
            Elements?.Add(panel);
            SafeActions.InvokeActionSafe(OnElementCreated, this, panel);
            return panel;
        }

        /// <summary>
        /// Creates a sub-panel with a name and a hex color input.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="hexColor">The color in hexadecimal.</param>
        /// <code>"Example: #00CA11 for green."</code>
        /// <returns>A new sub-panel in your page.</returns>
        public SubPanelElement CreateSubPanel(string name, string hexColor)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color);
            return CreateSubPanel(name, color);
        }

        /// <summary>
        /// Creates a function element that can be used to execute actions when pressed.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="color">The name color of the element.</param>
        /// <param name="action">The action that will be executed when pressed.
        /// <code>Example: () => ExampleMethod()</code></param>
        /// <returns>A function element.</returns>
        public FunctionElement CreateFunctionElement(string name, Color color, Action action)
        {
            FunctionElement element = new FunctionElement(name, color, action);
            Elements?.Add(element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        /// <summary>
        /// Creates a function element with a confirm option. When confirmed, the action will run.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="color">The name color of the element.</param>
        /// <param name="action">The action that will be executed when pressed.
        /// <param name="confirmText">The text that will be displayed before you confirm.</param>
        /// <code>Example: () => ExampleMethod()</code></param>
        /// <returns>A function element with a confirm option.</returns>
        public FunctionElement CreateFunctionElement(string name, Color color, Action action, string confirmText = "")
        {
            FunctionElement element = new FunctionElement(name, color, action, confirmText);
            Elements?.Add(element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        /// <summary>
        /// Creates a function element that can be used to execute actions when pressed.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="hexColor">The name hex color of the element.
        /// <code>"Example: "#00CA11" for green."</code> </param>
        /// <param name="action">The action that will be executed when pressed.
        /// <code>Example: () => ExampleMethod()</code> </param>
        /// <returns>A function element with a hex color.</returns>
        public FunctionElement CreateFunctionElement(string name, string hexColor, Action action)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color);
            return CreateFunctionElement(name, color, action);
        }

        /// <summary>
        /// Creates a bool element that enables or disables a boolean.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="color">The name color of the element.</param>
        /// <param name="value">The starting boolean value.</param>
        /// <param name="action">The method to execute with a boolean parameter.
        /// <code>Example: (bool) => ExampleMethod(bool);</code></param>
        /// <returns>A bool element.</returns>
        public BoolElement CreateBoolElement(string name, Color color, bool value, Action<bool> action = null)
        {
            BoolElement element = new BoolElement(name, color, value, action);
            Elements?.Add(element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        /// <summary>
        /// Creates a bool element that enables or disables a boolean.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="hexColor">The name hex color of the element. <code>"Example: "#00CA11" for green."</code> </param>
        /// <param name="value">The starting boolean value. <code>Example: (bool) => ExampleMethod(bool);</code> </param>
        /// <param name="action">The method to execute with a boolean parameter. <code>Example: (bool) => ExampleMethod(bool);</code></param>
        /// <returns>A bool element with a hex color.</returns>
        public BoolElement CreateBoolElement(string name, string hexColor, bool value, Action<bool> action = null)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color);
            return CreateBoolElement(name, color, value, action);
        }

        /// <summary>
        /// Creates an int element that can be incremented up or down, with a range.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="color">The name color of the element.</param>
        /// <param name="startValue">The starting value.</param>
        /// <param name="increment">The value that will be increased/decreased to the starting value.</param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="action">The method to execute with a integer parameter. <code>Example: (int) => ExampleMethod(int);</code></param>
        /// <returns>An integer element.</returns>
        public IntElement CreateIntElement(string name, Color color, int startValue, int increment, int minValue, int maxValue, Action<int> action = null)
        {
            IntElement element = new IntElement(name, color, startValue, increment, minValue, maxValue, action);
            Elements?.Add(element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        /// <summary>
        /// Creates an int element that can be incremented up or down, with a range.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="hexColor">The hex color of the element. <code>"Example: "#00CA11" for green."</code></param>
        /// <param name="startValue">The starting value.</param>
        /// <param name="increment">The value that will be increased/decreased to the starting value.</param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="action">The method to execute with a integer parameter. <code>Example: (int) => ExampleMethod(int);</code></param>
        /// <returns>An integer element with a hex color.</returns>
        public IntElement CreateIntElement(string name, string hexColor, int startValue, int increment, int minValue, int maxValue, Action<int> action = null)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color);
            return CreateIntElement(name, color, startValue, increment, minValue, maxValue, action);
        }

        /// <summary>
        /// Creates an element that holds enum types that can be changed.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="name">The name of the element.</param>
        /// <param name="color">The name color of the element.</param>
        /// <param name="action">The method that executes with an enum parameter. <code>Example: (enum) => ExampleMethod(enum);</code></param>
        /// <returns>An enum element.</returns>
        public EnumElement<T> CreateEnumElement<T>(string name, Color color, Action<T> action = null) where T : Enum
        {
            EnumElement<T> element = new EnumElement<T>(name, color, action);
            Elements?.Add(element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        /// <summary>
        /// Creates an element that holds enum types that can be changed.
        /// </summary>
        /// <typeparam name="T">Enum type.</typeparam>
        /// <param name="name">The name of the element.</param>
        /// <param name="hexColor">The hex color of the element. <code>"Example: "#00CA11" for green."</code></param>
        /// <param name="action">The method that executes with an enum parameter. <code>Example: (enum) => ExampleMethod(enum);</code></param>
        /// <returns>An enum element with a hex name color.</returns>
        public EnumElement<T> CreateEnumElement<T>(string name, string hexColor, Action<T> action = null) where T : Enum
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color);
            return CreateEnumElement<T>(name, color, action);
        }

        /// <summary>
        /// Creates an float element that can be incremented up or down, with a range.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="color">The name color of the element.</param>
        /// <param name="startValue">The starting value.</param>
        /// <param name="increment">The value that will be increased/decreased to the starting value.</param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="action">The method to execute with a float parameter. <code>Example: (float) => ExampleMethod(float);</code></param>
        /// <returns>A float element.</returns>
        public FloatElement CreateFloatElement(string name, Color color, float startValue, float increment, float minValue, float maxValue, Action<float> action = null)
        {
            FloatElement element = new FloatElement(name, color, startValue, increment, minValue, maxValue, action);
            Elements?.Add(element);
            SafeActions.InvokeActionSafe(OnElementCreated, this, element);
            return element;
        }

        /// <summary>
        /// Creates an float element that can be incremented up or down, with a range.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="hexColor">The hex color of the element. <code>"Example: "#00CA11" for green."</code></param>
        /// <param name="startValue">The starting value.</param>
        /// <param name="increment">The value that will be increased/decreased to the starting value.</param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="action">The method to execute with a float parameter. <code>Example: (float) => ExampleMethod(float);</code></param>
        /// <returns>A float element with a hex color.</returns>
        public FloatElement CreateFloatElement(string name, string hexColor, float startValue, float increment, float minValue, float maxValue, Action<float> action = null)
        {
            ColorUtility.DoTryParseHtmlColor(hexColor, out Color32 color);
            return CreateFloatElement(name, color, startValue, increment, minValue, maxValue, action);
        }

        public override void OnSelectElement()
        {
            SafeActions.InvokeActionSafe(OnElementSelected, this, this);
        }
    }
}
