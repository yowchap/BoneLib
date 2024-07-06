using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Il2CppSLZ.Bonelab;

using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Reflection;
using BoneLib.BoneMenu.UI;
using Harmony;

namespace BoneLib.BoneMenu
{
    internal static class MenuBootstrap
    {
        public static AssetBundle Bundle { get; private set; }

        public static PreferencesPanelView panelView;
        public static GameObject optionsPanel;

        public static Transform OptionsGrid
        {
            get
            {
                if (_optionsGrid is null || _optionsGrid.WasCollected)
                    return null;

                return _optionsGrid;
            }
        }

        public static GameObject pagePrefab;
        public static GameObject functionPrefab;
        public static GameObject intPrefab;
        public static GameObject floatPrefab;
        public static GameObject boolPrefab;
        public static GameObject enumPrefab;
        public static GameObject stringPrefab;
        public static GameObject rootButtonPrefab;
        public static Texture2D defaultBackgroundTexture;

        private static GameObject _optionButton;
        private static Transform _optionsGrid;
        private static Button _optionButtonComponent;
        private static GameObject _menuBackground;

        public static void InitializeBundles()
        {
            string bundlePath = "BoneLib.Resources.";
            string targetBundle = HelperMethods.IsAndroid() ? "bonemenu.android.pack" : "bonemenu.pack";

            Bundle = HelperMethods.LoadEmbeddedAssetBundle(Assembly.GetExecutingAssembly(), bundlePath + targetBundle);
            Bundle.hideFlags = HideFlags.DontUnloadUnusedAsset;

            pagePrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "[BoneMenu] - Canvas");
            functionPrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "FunctionElement");
            intPrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "IntElement");
            floatPrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "FloatElement");
            enumPrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "EnumElement");
            stringPrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "StringElement");
            boolPrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "BoolElement");
            rootButtonPrefab = HelperMethods.LoadPersistentAsset<GameObject>(Bundle, "MenuButton");
            defaultBackgroundTexture = HelperMethods.LoadPersistentAsset<Texture2D>(Bundle, "sprite_blackGrid_blur");
        }

        public static void InitializeReferences()
        {
            panelView = Player.UIRig.popUpMenu.preferencesPanelView;
            var page = GameObject.Instantiate(pagePrefab, panelView.transform);
            page.transform.localPosition = Vector3.zero;
            page.transform.localRotation = Quaternion.identity;
            optionsPanel = panelView.pages[panelView.defaultPage];
            _optionsGrid = optionsPanel.transform.Find("grid_Options");
            _menuBackground = panelView.transform.Find("image_bgFade").gameObject;

            _optionButton = SetupElement(rootButtonPrefab, OptionsGrid, true);
            ModifyBaseUI();
        }

        public static void ResetGameMenu()
        {
            _menuBackground.SetActive(true);
        }

        private static void ModifyBaseUI()
        {
            System.Action optionButtonAction = () =>
            {
                panelView.PAGESELECT(11);
                _menuBackground.SetActive(false);
                Menu.OpenPage(Page.Root);
            };

            _optionButtonComponent = _optionButton.GetComponent<Button>();
            _optionButtonComponent.onClick.AddListener(optionButtonAction);

            InjectPage();
        }

        private static void InjectPage()
        {
            Il2CppReferenceArray<GameObject> refArray = new Il2CppReferenceArray<GameObject>(12);
            for (int i = 0; i <= 10; i++)
            {
                refArray[i] = panelView.pages[i];
            }

            refArray[11] = GUIMenu.Instance.gameObject;

            panelView.pages = refArray;
        }

        private static GameObject SetupElement(GameObject objectToCreate, Transform parent, bool startActive)
        {
            GameObject newInstance = GameObject.Instantiate(objectToCreate, parent);
            newInstance.SetActive(startActive);
            newInstance.transform.SetSiblingIndex(5);

            return newInstance;
        }
    }
}
