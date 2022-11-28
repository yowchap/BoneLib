using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using SLZ.Rig;
using SLZ.UI;
using BoneLib.BoneMenu.UI;

namespace BoneLib.BoneMenu
{
    internal static class DataManager
    {
        public static class Bundles
        {
            public static void Init()
            {
                _bundleObjects = new List<GameObject>();
                _bundle = GetEmbeddedBundle();
                _bundle.hideFlags = HideFlags.DontUnloadUnusedAsset;

                var assets = bundle.LoadAllAssets();

                foreach (var asset in assets)
                {
                    if (asset.TryCast<GameObject>() != null)
                    {
                        var go = asset.Cast<GameObject>();
                        go.hideFlags = HideFlags.DontUnloadUnusedAsset;
                        _bundleObjects.Add(go);
                    }
                }
            }

            public static readonly AssetBundle bundle;
            public static IReadOnlyList<GameObject> BundleObjects { get => _bundleObjects.AsReadOnly(); }

            private static AssetBundle _bundle;
            private static List<GameObject> _bundleObjects;

            public static GameObject FindBundleObject(string name)
            {
                return _bundleObjects.Find(x => x.name == name);
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
            internal static void FindReferences()
            {
                _rigManager = BoneLib.Player.GetRigManager().GetComponent<RigManager>();
                _uiRig = _rigManager.uiRig;
            }

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
                    if(_rigManager is null || _rigManager.WasCollected)
                    {
                        return null;
                    }

                    return _uiRig;
                }
            }

            internal static RigManager _rigManager;
            internal static UIRig _uiRig;
        }

        public static class UI
        {
            public static PreferencesPanelView panelView;
            public static GameObject optionsPanel;
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

            public static GameObject pagePrefab = Bundles.FindBundleObject("Element_Page");
            public static GameObject categoryPrefab = Bundles.FindBundleObject("Element_Category");
            public static GameObject functionPrefab = Bundles.FindBundleObject("Element_Function");
            public static GameObject valuePrefab = Bundles.FindBundleObject("Element_Value");
            public static GameObject valueListPrefab = Bundles.FindBundleObject("Element_ValueList");
            public static GameObject togglePrefab = Bundles.FindBundleObject("Element_Toggle");
            public static GameObject subPanelPrefab = Bundles.FindBundleObject("Element_SubPanel");
            public static GameObject emptyPrefab = Bundles.FindBundleObject("Element_Empty");

            public static GameObject bmButtonObject = Bundles.FindBundleObject("BoneMenuButton");

            public static GameObject MainPage { get => _mainPage; }

            static GameObject _mainPage;
            static GameObject _optionButton;

            static Transform _optionsGrid;

            static Button _optionButtonComponent;

            public static void Init()
            {
                _optionButton = SetupElement(bmButtonObject, OptionsGrid, true);

                ModifyBaseUI();
            }

            public static void InitializeReferences()
            {
                Player._rigManager = BoneLib.Player.GetRigManager().GetComponent<RigManager>();
                Player._uiRig = Player.RigManager.uiRig;
                panelView = Player.UIRig.popUpMenu.preferencesPanelView;
                optionsPanel = panelView.pages[panelView.defaultPage];
                _optionsGrid = optionsPanel.transform.Find("grid_Options");
            }

            public static void AddComponents()
            {
                pagePrefab.GetComponent<UIPage>();
                categoryPrefab.GetComponent<UICategoryField>();
                functionPrefab.GetComponent<UIFunctionField>();
                valuePrefab.GetComponent<UIValueField>();
            }

            static void ModifyBaseUI()
            {
                Action optionButtonAction = () =>
                {
                    panelView.PAGESELECT(9);
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
                    refArray[i] = panelView.pages[i];
                }

                refArray[9] = UIManager.Instance.MainPage.gameObject;

                panelView.pages = refArray;
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
