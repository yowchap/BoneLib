using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPage : UIElement
    {
        public UIPage(IntPtr ptr) : base(ptr) { }

        public List<UIElement> Elements { get; private set; } = new List<UIElement>();

        private Transform _elementGrid;
        private Transform _returnArrow;

        private Button _returnButton;

        private void Awake()
        {
            _elementGrid = transform.Find("Viewport/ElementGrid");
            _returnArrow = transform.Find("Return");

            _returnButton = _returnArrow.GetComponent<Button>();
        }

        private void Start()
        {
            Action returnAction = () =>
            {
                var category = (MenuCategory)_element;

                if (category.Parent != null)
                {
                    MenuManager.SelectCategory(category.Parent);
                }
                else
                {
                    MenuManager.SelectCategory(MenuManager.RootCategory);
                }
            };

            if (_returnButton != null)
            {
                _returnButton.onClick.AddListener(returnAction);
            }
        }

        public void Draw()
        {
            ClearDraw();

            var activeCategory = MenuManager.ActiveCategory;

            if (activeCategory == null)
            {
                return;
            }

            SetText(activeCategory.Name);

            for (int i = 0; i < activeCategory.Elements.Count; i++)
            {
                var element = activeCategory.Elements[i];
                UIElement uiElement = null;

                if (element.Type == ElementType.Type_Category)
                {
                    var obj = UIManager.Instance.CategoryPool.Spawn(_elementGrid.transform, true);
                    uiElement = obj.GetComponent<UICategoryField>();
                    uiElement.AssignElement(element);
                }

                if (element.Type == ElementType.Type_Function)
                {
                    var obj = UIManager.Instance.FunctionPool.Spawn(_elementGrid.transform, true);
                    uiElement = obj.GetComponent<UIFunctionField>();
                    uiElement.AssignElement(element);
                }

                if (element.Type == ElementType.Type_Value)
                {
                    var obj = UIManager.Instance.ValuePool.Spawn(_elementGrid.transform, true);
                    uiElement = obj.GetComponent<UIValueField>();
                    uiElement.AssignElement(element);
                }

                Elements?.Add(uiElement);
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

            Elements.Clear();
        }
    }
}
