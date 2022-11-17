using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPage : UIElement
    {
        public UIPage(IntPtr ptr) : base(ptr) { }

        public IReadOnlyList<UIElement> Elements => _elements.AsReadOnly();
        private List<UIElement> _elements;

        private Transform elementGrid;
        private Transform returnArrow;

        private Button returnButton;

        private void Awake()
        {
            elementGrid = transform.Find("Viewport/ElementGrid");
            returnArrow = transform.Find("Return");

            returnButton = returnArrow.GetComponent<Button>();
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
                    if(MenuManager.ActiveCategory == MenuManager.RootCategory)
                    {
                        // return to base game options menu
                        gameObject.SetActive(false);
                        DataManager.UI.PanelView.PAGESELECT(0);
                    }
                    else
                    {
                        MenuManager.SelectCategory(MenuManager.RootCategory);
                    }
                }
            };

            if (returnButton != null)
            {
                returnButton.onClick.AddListener(returnAction);
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
                AssignUIElement(element);
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

            _elements.Clear();
        }

        private void AssignUIElement(MenuElement element)
        {
            UIElement uiElement = null;

            var categoryPool = UIManager.Instance.CategoryPool;
            var functionPool = UIManager.Instance.FunctionPool;
            var valuePool = UIManager.Instance.ValuePool;
            var valueListPool = UIManager.Instance.ValueListPool;
            var togglePool = UIManager.Instance.TogglePool;

            switch (element.Type)
            {
                case ElementType.Category: categoryPool.Spawn<UICategoryField>(elementGrid.transform, true).AssignElement(element); break;
                case ElementType.Function: functionPool.Spawn<UIFunctionField>(elementGrid.transform, true).AssignElement(element); break;
                case ElementType.Value: valuePool.Spawn<UIValueField>(elementGrid.transform, true).AssignElement(element); break;
                case ElementType.ValueList: valueListPool.Spawn<UIDropdownField>(elementGrid.transform, true).AssignElement(element); break;
                case ElementType.Toggle: togglePool.Spawn<UIToggleField>(elementGrid.transform, true).AssignElement(element); break;
            }

            Elements?.Add(uiElement);
        }
    }
}
