using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using SLZ.Rig;
using SLZ.UI.Radial;

using BoneLib.BoneMenu.UI;

using TMPro;

namespace BoneLib.BoneMenu
{
    public static class MenuManager
    {
        public static MenuCategory RootCategory { get => _rootCategory; }
        public static MenuCategory ActiveCategory { get => _activeCategory; }
        public static List<MenuCategory> Categories { get => _categories; }

        public static Action<MenuCategory> OnCategoryCreated;
        public static Action<MenuCategory> OnCategorySelected;

        private static List<MenuCategory> _categories = new List<MenuCategory>();
        private static MenuCategory _rootCategory = null;
        private static MenuCategory _activeCategory = null;

        public static MenuCategory CreateCategory(string name, Color color)
        {
            MenuCategory category = RootCategory.CreateCategory(name, color);
            _categories?.Add(category);
            SafeActions.InvokeActionSafe(OnCategoryCreated, category);
            return category;
        }

        public static void SelectCategory(MenuCategory category)
        {
            if (category == null)
            {
                return;
            }

            _activeCategory = category;
            SafeActions.InvokeActionSafe(OnCategorySelected, _activeCategory);
        }

        public static void SetRoot(MenuCategory root)
        {
            _rootCategory = root;
        }

        public static MenuCategory GetCategory(string name)
        {
            foreach (var category in Categories)
            {
                if (category.Name == name)
                {
                    return category;
                }
            }

            return null;
        }
    }
}