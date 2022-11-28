using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using BoneLib.BoneMenu.Elements;

namespace BoneLib.BoneMenu
{
    public static class MenuManager
    {
        public static IReadOnlyList<MenuCategory> Categories { get => _categories.AsReadOnly(); }
        public static MenuCategory RootCategory { get => _rootCategory; }
        public static MenuCategory ActiveCategory { get => _activeCategory; }

        public static Action<MenuCategory> OnCategoryCreated;
        public static Action<MenuCategory> OnCategorySelected;

        private static List<MenuCategory> _categories = new List<MenuCategory>();
        private static MenuCategory _rootCategory = null;
        private static MenuCategory _activeCategory = null;

        /// <summary>
        /// Creates a category inside of the root category.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="color">The name color of the category.</param>
        /// <returns>A new category in the root category.</returns>
        public static MenuCategory CreateCategory(string name, Color color)
        {
            MenuCategory category = RootCategory.CreateCategory(name, color);
            _categories.Add(category);
            SafeActions.InvokeActionSafe(OnCategoryCreated, category);
            return category;
        }

        /// <summary>
        /// Creates a new category inside of the root category.
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <param name="hexColor">The name color in hex. <code>"Example: #00CA11 for green."</code></param>
        /// <returns>A new category in the root category, with a hex color.</returns>
        public static MenuCategory CreateCategory(string name, string hexColor)
        {
            MenuCategory category = RootCategory.CreateCategory(name, hexColor);
            _categories.Add(category);
            SafeActions.InvokeActionSafe(OnCategoryCreated, category);
            return category;
        }

        /// <summary>
        /// Selects a category and displays it on the UI.
        /// </summary>
        /// <param name="category">The category you want to select.</param>
        public static void SelectCategory(MenuCategory category)
        {
            if (category == null)
            {
                return;
            }

            _activeCategory = category;
            SafeActions.InvokeActionSafe(OnCategorySelected, _activeCategory);
        }

        /// <summary>
        /// Sets the root of the menu manager, so all categories created will display here.
        /// </summary>
        /// <param name="root">The root page that BoneMenu will use to create elements.</param>
        /// <remarks><b>Note:</b>
        /// This will mean that setting the root will redirect all other categories to the set root, and BoneMenu will treat it
        /// as if it were the beginning category.
        /// </remarks>
        public static void SetRoot(MenuCategory root)
        {
            _rootCategory = root;
        }

        public static MenuCategory GetCategory(string name)
        {
            return _categories.Find((match) => match.Name == name);
        }
    }
}