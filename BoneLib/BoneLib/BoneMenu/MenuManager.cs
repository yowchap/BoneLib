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
        public static class Bundles
        {
            public static void Init()
            {
                BundleObjects = new List<GameObject>();
                _bundle = GetEmbeddedBundle();
                _bundle.hideFlags = HideFlags.DontUnloadUnusedAsset;

                var assets = Bundle.LoadAllAssets();

                foreach (var asset in assets)
                {
                    if (asset.TryCast<GameObject>() != null)
                    {
                        var go = asset.Cast<GameObject>();
                        go.hideFlags = HideFlags.DontUnloadUnusedAsset;
                        BundleObjects.Add(go);
                    }
                }
            }

            public static AssetBundle Bundle { get => _bundle; }
            public static List<GameObject> BundleObjects { get; private set; }

            private static AssetBundle _bundle;

            public static GameObject FindBundleObject(string name)
            {
                return BundleObjects.Find(x => x.name == name);
            }

            static AssetBundle GetEmbeddedBundle()
            {
                AssetBundle bundle = null;
                Assembly assembly = Assembly.GetExecutingAssembly();

                var manifestNames = assembly.GetManifestResourceNames();

                using (Stream stream = assembly.GetManifestResourceStream(manifestNames.First(x => x.Contains("BoneLib.Resources.bonemenu.pack"))))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        bundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
                    }
                }

                return bundle;
            }
        }

        public static class Player
        {
            public static RigManager RigManager { get => BoneLib.Player.GetRigManager().GetComponent<RigManager>(); }
            public static UIRig UIRig { get => RigManager.uiRig; }
        }

        public static class UI
        {
            public static PreferencesPanelView PanelView { get => Player.UIRig.popUpMenu.preferencesPanelView; }
            public static GameObject OptionsPanel { get => PanelView.pages[0]; }
            public static Transform OptionsGrid { get => OptionsPanel.transform.Find("grid_Options"); }

            public static GameObject PageObject = Bundles.FindBundleObject("[BoneMenu] - Generic Page");
            public static GameObject CategoryFieldObject = Bundles.FindBundleObject("[BoneMenu] - Category Element");
            public static GameObject FunctionFieldObject = Bundles.FindBundleObject("[BoneMenu] - Function Property");
            public static GameObject NumberFieldObject = Bundles.FindBundleObject("[BoneMenu] - Number Property");

            public static GameObject BMButtonObject = Bundles.FindBundleObject("[BoneMenu] - Option Button");

            public static GameObject MainPage { get => _mainPage; }

            static GameObject _mainPage;
            static GameObject _optionButton;

            static Button _optionButtonComponent;
            static Button _arrowButtonComponent;

            public static void Init()
            {
                _mainPage = SetupElement(PageObject, PanelView.transform, false);
                _optionButton = SetupElement(BMButtonObject, OptionsGrid, true);

                ModifyBaseUI();
            }

            public static void AddComponents()
            {
                PageObject.AddComponent<UIPage>();
                CategoryFieldObject.AddComponent<UICategoryField>();
                FunctionFieldObject.AddComponent<UIFunctionField>();
                NumberFieldObject.AddComponent<UIValueField>();
            }

            static void ModifyBaseUI()
            {
                _optionButtonComponent = _optionButton.GetComponent<Button>();
                _optionButtonComponent.onClick.AddListener(new Action(() => PanelView.PAGESELECT(6)));

                _arrowButtonComponent = _mainPage.transform.Find("[Button] - Return").GetComponent<Button>();
                _arrowButtonComponent.onClick.AddListener(new Action(() => PanelView.PAGESELECT(PanelView.defaultPage)));

                var list = new UnhollowerBaseLib.Il2CppReferenceArray<GameObject>(7);

                for (int i = 0; i < 6; i++)
                {
                    list[i] = PanelView.pages[i];
                }

                list[6] = _mainPage;

                PanelView.pages = list;
            }

            static GameObject SetupElement(GameObject objectToCreate, Transform parent, bool startActive)
            {
                GameObject newInstance = GameObject.Instantiate(objectToCreate, parent);
                newInstance.SetActive(startActive);
                newInstance.transform.SetSiblingIndex(5);

                return newInstance;
            }
        }

        public static Action<MenuCategory> OnCategoryCreated;
        public static Action<MenuCategory> OnCategorySelected;

        private static List<MenuCategory> _categories = new List<MenuCategory>();
        private static MenuCategory _activeCategory = null;

        public static void Init()
        {
            UI.Init();

            OnCategoryCreated += OnCreateCategory;
        }

        public static void OnCreateCategory(MenuElement element)
        {
            var test = GameObject.Instantiate(UI.CategoryFieldObject, UI.MainPage.transform.Find("Viewport/ElementGrid"));
            var testco = test.GetComponent<UICategoryField>();
            testco.AssignElement(element);
        }

        public static MenuCategory CreateCategory(string name, Color color)
        {
            MenuCategory category = new MenuCategory(name, color);
            _categories?.Add(category);
            SafeActions.InvokeActionSafe(OnCategoryCreated, category);
            return category;
        }

        public static void SelectCategory(MenuCategory category)
        {
            if(category == null)
            {
                return;
            }

            _activeCategory = category;
            SafeActions.InvokeActionSafe(OnCategorySelected, _activeCategory);
        }
    }
}