using System;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

using SLZ.UI;

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

        private ButtonHoverClick subpanelFeedback;

        private void Awake()
        {
            emptyObjects = new List<GameObject>();
            dropdownButton = transform.Find("Button").GetComponent<Button>();
            gridObject = transform.Find("Grid").gameObject;

            subpanelFeedback = dropdownButton.GetComponent<ButtonHoverClick>();

            subpanelFeedback.feedback_audio = DataManager.Player.UIRig.feedbackAudio;
            subpanelFeedback.feedback_tactile = DataManager.Player.UIRig.feedbackTactile;

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

            var subPanel = (SubPanelElement)_element;

            if (subPanel == null)
            {
                return;
            }

            for (int i = 0; i < subPanel.Elements.Count; i++)
            {
                var element = subPanel.Elements[i];
                AssignUIElement(element);
            }

            for (int i = 0; i < subPanel.Elements.Count - 1; i++)
            {
                var empty = UIManager.Instance.EmptyPool.Spawn(UIManager.Instance.MainPage.ElementGrid, transform.GetSiblingIndex() + 1, true);
                emptyObjects?.Add(empty.gameObject);
            }
        }

        public void ClearDraw()
        {
            foreach (var element in Elements)
            {
                var poolee = element.GetComponent<UIPoolee>();
                poolee.Return();
                poolee.gameObject.SetActive(false);
            }

            foreach(var empty in emptyObjects)
            {
                var poolee = empty.GetComponent<UIPoolee>();
                poolee.Return();
                poolee.gameObject.SetActive(false);
            }

            Elements.Clear();
            emptyObjects.Clear();
        }

        private void AssignUIElement(MenuElement element)
        {
            UIElement uiElement = null;

            if (element.Type == ElementType.Category)
            {
                var obj = UIManager.Instance.CategoryPool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UICategoryField>();
                uiElement.AssignElement(element);
            }

            if (element.Type == ElementType.Function || element.Type == ElementType.Confirmer)
            {
                var obj = UIManager.Instance.FunctionPool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UIFunctionField>();
                uiElement.AssignElement(element);
            }

            if (element.Type == ElementType.Value)
            {
                var obj = UIManager.Instance.ValuePool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UIValueField>();
                uiElement.AssignElement(element);
            }

            if (element.Type == ElementType.Toggle)
            {
                var obj = UIManager.Instance.TogglePool.Spawn(gridObject.transform, true);
                uiElement = obj.GetComponent<UIToggleField>();
                uiElement.AssignElement(element);
            }

            Elements?.Add(uiElement);
        }
    }
}
