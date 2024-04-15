using SLZ.Rig;
using SLZ.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using SLZ.Props.Weapons;
using BoneLib.Notifications;
using UnityEngine.UI;
using UnityEngine;
using UnhollowerBaseLib;
using HarmonyLib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Runtime.Remoting.Messaging;
using TMPro;

namespace BoneLib.RadialMenu
{
    public static class CustomRadialMenu
    {
        private static readonly List<RadialCategory> _categories = new();

        private static bool _hasLoaded = false;

        private static TextMeshProUGUI _popUpText;

        private static readonly Sprite[] RadialMenuSprites = new Sprite[8];
        private static int _currentCategoryIndex = 0;

        private static RadialCategory _defaultCategory;

        internal static void AddRadialCategory(RadialCategory category)
        {
            _categories.Add(category);
        }

        private static RadialButton _addRemove;
        internal static void InitializeRadialMenu()
        {
            Hooking.CreateHook(typeof(Player_Health).GetMethod(nameof(Player_Health.MakeVignette), AccessTools.all), typeof(CustomRadialMenu).GetMethod(nameof(OnPlayerCreated), AccessTools.all));
            Hooking.CreateHook(typeof(PopUpMenuView).GetMethod(nameof(PopUpMenuView.RemoveSpawnMenu), AccessTools.all), typeof(CustomRadialMenu).GetMethod(nameof(OnRemoveSpawnMenu), AccessTools.all));
            Hooking.CreateHook(typeof(PopUpMenuView).GetMethod(nameof(PopUpMenuView.RemoveMagEjectMenu), AccessTools.all), typeof(CustomRadialMenu).GetMethod(nameof(OnRemoveMagEject), AccessTools.all));

            Hooking.OnLevelUnloaded += OnPlayerUnloaded;

            Hooking.OnPopUpMenuOpenPostFix += OnMenuOpened;
            Hooking.OnPopUpMenuClosedPostFix += OnMenuClosed;
        }

        private static void OnRemoveMagEject()
        {
            if (_defaultCategory.Buttons.Any(x => x.PageItem.name == "Eject"))
            {
                _defaultCategory.Buttons.Remove(_defaultCategory.Buttons.First(x => x.PageItem.name == "Eject"));
            }
        }
        private static void OnRemoveSpawnMenu()
        {
            if (_defaultCategory.Buttons.Any(x => x.PageItem.name == "Utilities"))
            {
                _defaultCategory.Buttons.Remove(_defaultCategory.Buttons.First(x => x.PageItem.name == "Utilities"));
            }
        }

        private static void OnMenuClosed(PopUpMenuView menu)
        {
            if (!_hasLoaded)
                return;

            menu.radialPageView.m_HomePage.items.Clear();

            foreach (var button in _defaultCategory.Buttons)
            {
                menu.radialPageView.m_HomePage.items.Add(button.PageItem);
            }
        }
        private static void OnMenuOpened(PopUpMenuView menu)
        {
            if (!_hasLoaded)
                return;

            _currentCategoryIndex = 0;

            var rightCycleItem = new PageItem("---->", PageItem.Directions.SOUTHEAST, (Action)(() => CycleRight()));

            menu.radialPageView.m_HomePage.items.Add(rightCycleItem);

            _defaultCategory.Buttons.Clear();

            List<RadialButton> buttonList = new();

            foreach (var pageItem in menu.radialPageView.m_HomePage.items)
            {
                var button = new RadialButton(pageItem.name, pageItem.m_Callback, pageItem.direction);

                if (pageItem.name != "---->")
                {
                    var sprite = RadialMenuSprites[GetRadialIndexFromDirection(pageItem.direction)];

                    button.Icon = sprite;
                }

                buttonList.Add(button);
            }

            foreach (var button in buttonList)
            {
                _defaultCategory.TryAddButton(button);
            }

            RefreshRadialCategory(_defaultCategory);
        }

        public static void CycleLeft()
        {
            if (_currentCategoryIndex > 0)
                _currentCategoryIndex--;

            RefreshRadialCategory(_categories[_currentCategoryIndex]);
        }

        public static void CycleRight()
        {
            if (_currentCategoryIndex < _categories.Count - 1)
                _currentCategoryIndex++;

            RefreshRadialCategory(_categories[_currentCategoryIndex]);
        }

        public static RadialCategory ActiveCategory { get => _categories[_currentCategoryIndex];}
        internal static void RefreshRadialCategory(RadialCategory category)
        {
            // Remove the indicator for the cancel button
            BoneLib.Player.uiRig.popUpMenu.radialPageView.cancelButton.gameObject.SetActive(false);

            // Set the text for the category
            _popUpText.SetText(category.Name);
            _popUpText.color = category.Color;

            // Setup the buttons for the category
            Player.uiRig.popUpMenu.radialPageView.m_HomePage.items.Clear();
            foreach (var item in category.Buttons)
            {
                Player.uiRig.popUpMenu.radialPageView.m_HomePage.items.Add(item.PageItem);

                // Set the icon for the button
                var button = Player.uiRig.popUpMenu.radialPageView.buttons[GetRadialIndexFromDirection(item.PageItem.direction)];

                var icon = button.icon;

                var image = icon.GetComponent<Image>();

                if (item.Icon != null)
                {
                    image.sprite = item.Icon;

                    image.color = Color.white;
                } else
                {
                    image.sprite = null;

                    image.color = Color.clear;
                }
            }

            Player.uiRig.popUpMenu.radialPageView.Render(Player.uiRig.popUpMenu.radialPageView.m_HomePage);
        }

        private static void OnPlayerCreated()
        {
            _hasLoaded = true;

            // Setup the default category
            _defaultCategory = new()
            {
                Name = ""
            };

            if (_categories.Any(x => x.Name == ""))
            {
                _categories.Remove(_categories.First(x => x.Name == ""));
            }

            _categories.Insert(0, _defaultCategory);

            // Setup Radial sprites
            for (int i = 0; i <= 7; i++)
            {
                RadialMenuSprites[i] = Player.uiRig.popUpMenu.radialPageView.buttons[i].icon.GetComponent<Image>().sprite;

                MelonLogger.Msg($"Sprite {i}: {RadialMenuSprites[i]}");
            }

            // Setup the text object
            GameObject textObject = new("PopUpText");

            _popUpText = textObject.AddComponent<TextMeshProUGUI>();

            _popUpText.text = "";

            _popUpText.enableAutoSizing = true;
            _popUpText.fontSizeMax = 100;
            _popUpText.fontSizeMin = 1;
            _popUpText.alignment = TextAlignmentOptions.Center;
            _popUpText.fontStyle = FontStyles.Bold;

            _popUpText.color = Color.white;
            _popUpText.font = Player.uiRig.popUpMenu.radialPageView.buttons[0].textMesh.font;

            // Add the text object to the RectTransform
            textObject.transform.SetParent(Player.uiRig.popUpMenu.radialPageView.TextCanvas.transform, false);

            RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
            textRectTransform.anchoredPosition = new Vector2(0, 0); 
            textRectTransform.sizeDelta = new Vector2(150, 150); 

            var canvas = Player.uiRig.popUpMenu.radialPageView.TextCanvas.GetComponent<Canvas>();
        }

        private static void OnPlayerUnloaded()
        {
            _hasLoaded = false;
        }

        public static byte GetRadialIndexFromDirection(PageItem.Directions directions)
        {
            byte index = directions switch
            {
                PageItem.Directions.NORTH => 0,
                PageItem.Directions.NORTHEAST => 1,
                PageItem.Directions.EAST => 2,
                PageItem.Directions.SOUTHEAST => 3,
                PageItem.Directions.SOUTH => 4,
                PageItem.Directions.SOUTHWEST => 5,
                PageItem.Directions.WEST => 6,
                PageItem.Directions.NORTHWEST => 7,
                _ => 0,
            };
            return index;
        }
    }
}
