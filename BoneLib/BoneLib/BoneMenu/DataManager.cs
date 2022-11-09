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
    public static class DataManager
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

            public static GameObject PagePrefab = Bundles.FindBundleObject("Element_Page");
            public static GameObject CategoryPrefab = Bundles.FindBundleObject("Element_Category");
            public static GameObject FunctionPrefab = Bundles.FindBundleObject("Element_Function");
            public static GameObject ValuePrefab = Bundles.FindBundleObject("Element_Value");

            public static GameObject BMButtonObject = Bundles.FindBundleObject("BoneMenuButton");

            public static GameObject MainPage { get => _mainPage; }

            static GameObject _mainPage;
            static GameObject _optionButton;

            static Button _optionButtonComponent;
            static Button _arrowButtonComponent;

            public static void Init()
            {
                _mainPage = SetupElement(PagePrefab, PanelView.transform, false);
                _optionButton = SetupElement(BMButtonObject, OptionsGrid, true);

                ModifyBaseUI();
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
                    PanelView.ALLOFF();
                    MenuManager.SelectCategory(MenuManager.RootCategory);
                };

                _optionButtonComponent = _optionButton.GetComponent<Button>();
                _optionButtonComponent.onClick.AddListener(optionButtonAction);

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
    }
}
