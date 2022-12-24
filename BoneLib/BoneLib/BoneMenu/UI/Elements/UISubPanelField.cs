using BoneLib.BoneMenu.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UISubPanelField : UIElement
    {
        public UISubPanelField(System.IntPtr ptr) : base(ptr) { }

        public override ElementType Type => ElementType.SubPanel;

        public List<UIElement> Elements = new List<UIElement>();

        private Button dropdownButton;
        private GameObject gridObject;

        private List<GameObject> emptyObjects;

        private void Awake()
        {
            emptyObjects = new List<GameObject>();
            dropdownButton = transform.Find("Button").GetComponent<Button>();
            gridObject = transform.Find("Grid").gameObject;

            Initialize();
        }

        private void OnDisable()
        {
            ClearDraw();
        }

        private void Initialize()
        {
            Action onPressed = () =>
            {
                gridObject.gameObject.SetActive(!gridObject.gameObject.activeInHierarchy);
                Draw();
            };

            dropdownButton?.onClick.AddListener(onPressed);
        }

        public void Draw()
        {
            ClearDraw();

            if (!gridObject.gameObject.activeInHierarchy)
            {
                return;
            }

            SubPanelElement subPanel = (SubPanelElement)element;

            if (subPanel == null)
            {
                return;
            }

            for (int i = 0; i < subPanel.Elements.Count; i++)
            {
                MenuElement element = subPanel.Elements[i];
                AssignUIElement(element);
            }

            int emptyCount = HelperMethods.IsAndroid() ? subPanel.Elements.Count * 35 : subPanel.Elements.Count;

            for (int i = 0; i < emptyCount - 1; i++)
            {
                UIPoolee empty = UIManager.Instance.EmptyPool.Spawn(UIManager.Instance.MainPage.ElementGrid, transform.GetSiblingIndex() + 1, true);
                emptyObjects?.Add(empty.gameObject);
            }
        }

        public void ClearDraw()
        {
            foreach (UIElement element in Elements)
            {
                UIPoolee poolee = element.GetComponent<UIPoolee>();
                poolee.Return();
                poolee.gameObject.SetActive(false);
            }

            foreach (GameObject empty in emptyObjects)
            {
                UIPoolee poolee = empty.GetComponent<UIPoolee>();
                poolee.Return();
                poolee.gameObject.SetActive(false);
            }

            Elements.Clear();
            emptyObjects.Clear();
        }
        [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
        private void AssignUIElement(MenuElement element)
        {
            UIElement uiElement = null;

            if (element.Type == ElementType.Category)
            {
                UIPoolee obj = UIManager.Instance.CategoryPool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UICategoryField>();
                uiElement.AssignElement(element);
            }

            if (element.Type == ElementType.Function || element.Type == ElementType.Confirmer)
            {
                UIPoolee obj = UIManager.Instance.FunctionPool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UIFunctionField>();
                uiElement.AssignElement(element);
            }

            if (element.Type == ElementType.Value)
            {
                UIPoolee obj = UIManager.Instance.ValuePool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UIValueField>();
                uiElement.AssignElement(element);
            }

            if (element.Type == ElementType.Toggle)
            {
                UIPoolee obj = UIManager.Instance.TogglePool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UIToggleField>();
                uiElement.AssignElement(element);
            }

            Elements?.Add(uiElement);
        }
    }
}
