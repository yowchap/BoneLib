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

namespace BoneLib.BoneMenu
{
    internal static class DataManager
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
                Assembly assembly = Assembly.GetExecutingAssembly();

                using (var resourceStream = assembly.GetManifestResourceStream("BoneLib.Resources.bonemenu.pack"))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        resourceStream.CopyTo(memoryStream);
                        return AssetBundle.LoadFromMemory(memoryStream.ToArray());
                    }
                }
            }
        }

        public static class Player
        {
            public static RigManager RigManager
            {
                get
                {
                    if (_rigManager is null || _rigManager.WasCollected)
                    {
                        return null;
                    }

                    return _rigManager;
                }
            }
            public static UIRig UIRig
            {
                get
                {
                    if(_rigManager == null)
                    {
                        return null;
                    }

                    return _uiRig = _rigManager.uiRig;
                }
            }

            static RigManager _rigManager;
            static UIRig _uiRig;
        }

        public static class UI
        {
            public static PreferencesPanelView PanelView;
            public static GameObject OptionsPanel;
            public static Transform OptionsGrid
            {
                get
                {
                    if(_optionsGrid is null || _optionsGrid.WasCollected)
                    {
                        return null;
                    }

                    return _optionsGrid;
                }
            }

            public static GameObject PagePrefab = Bundles.FindBundleObject("Element_Page");
            public static GameObject CategoryPrefab = Bundles.FindBundleObject("Element_Category");
            public static GameObject FunctionPrefab = Bundles.FindBundleObject("Element_Function");
            public static GameObject ValuePrefab = Bundles.FindBundleObject("Element_Value");
            public static GameObject ValueListPrefab = Bundles.FindBundleObject("Element_ValueList");
            public static GameObject TogglePrefab = Bundles.FindBundleObject("Element_Toggle");

            public static GameObject BMButtonObject = Bundles.FindBundleObject("BoneMenuButton");

            public static GameObject MainPage { get => _mainPage; }

            static GameObject _mainPage;
            static GameObject _optionButton;

            static Transform _optionsGrid;

            static Button _optionButtonComponent;

            public static void Init()
            {
                _optionButton = SetupElement(BMButtonObject, OptionsGrid, true);

                ModifyBaseUI();
            }

            public static void InitializeReferences()
            {
                PanelView = Player.UIRig.popUpMenu.preferencesPanelView;
                OptionsPanel = PanelView.pages[PanelView.defaultPage];
                _optionsGrid = OptionsPanel.transform.Find("grid_Options");
            }

            public static void AddComponents()
            {
                PagePrefab.GetComponent<UIPage>();
                CategoryPrefab.GetComponent<UICategoryField>();
                FunctionPrefab.GetComponent<UIFunctionField>();
                ValuePrefab.GetComponent<UIValueField>();
            }

            static void ModifyBaseUI()
            {
                Action optionButtonAction = () =>
                {
                    MelonLoader.MelonLogger.Msg("test");
                    PanelView.PAGESELECT(9);
                    MenuManager.SelectCategory(MenuManager.RootCategory);
                };

                _optionButtonComponent = _optionButton.GetComponent<Button>();
                _optionButtonComponent.onClick.AddListener(optionButtonAction);

                InjectPage();
            }

            static void InjectPage()
            {
                var refArray = new UnhollowerBaseLib.Il2CppReferenceArray<GameObject>(10);

                for(int i = 0; i <= 8; i++)
                {
                    refArray[i] = PanelView.pages[i];
                }

                refArray[9] = UIManager.Instance.MainPage.gameObject;

                PanelView.pages = refArray;
            }

            static GameObject SetupElement(GameObject objectToCreate, Transform parent, bool startActive)
            {
                GameObject newInstance = GameObject.Instantiate(objectToCreate, parent);
                newInstance.SetActive(startActive);
                newInstance.transform.SetSiblingIndex(5);

                return newInstance;
            }
        }
    }
}
