using BoneLib.BoneMenu.Elements;
using SLZ.UI;
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

        [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
        public List<UIElement> Elements { get; private set; } = new List<UIElement>();
        public Transform ElementGrid { get; private set; }

        private UIGridEnable gridEnable;

        private Transform returnArrow;

        private Button returnButton;

        /// <summary>
        /// Dictionary that contains the proper pools for each element type.
        /// </summary>
        private Dictionary<ElementType, UIPool> elementTypes;

        private void Awake()
        {
            elementTypes = new Dictionary<ElementType, UIPool>()
            {
                { ElementType.Default, UIManager.Instance.FunctionPool },
                { ElementType.SubPanel, UIManager.Instance.SubPanelPool },
                { ElementType.Category, UIManager.Instance.CategoryPool },
                { ElementType.Function, UIManager.Instance.FunctionPool },
                { ElementType.Confirmer, UIManager.Instance.FunctionPool },
                { ElementType.Value, UIManager.Instance.ValuePool },
                { ElementType.Toggle, UIManager.Instance.TogglePool }
            };

            ElementGrid = transform.Find("Viewport/ElementGrid");
            returnArrow = transform.Find("Return");

            gridEnable = transform.Find("Viewport/ElementGrid").GetComponent<UIGridEnable>();
            returnButton = returnArrow.GetComponent<Button>();
        }

        private void Start()
        {
            Action returnAction = () =>
            {
                MenuCategory category = (MenuCategory)element;

                if (category.Parent != null)
                {
                    MenuManager.SelectCategory(category.Parent);
                }
                else
                {
                    if (MenuManager.ActiveCategory == MenuManager.RootCategory)
                    {
                        // return to base game options menu
                        gameObject.SetActive(false);
                        DataManager.UI.panelView.PAGESELECT(0);
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

            if (gridEnable != null)
            {
                gridEnable.enabled = true;
            }

            MenuCategory activeCategory = MenuManager.ActiveCategory;

            if (activeCategory == null)
            {
                return;
            }

            SetText(activeCategory.Name);

            for (int i = 0; i < activeCategory.Elements.Count; i++)
            {
                MenuElement element = activeCategory.Elements[i];
                AssignUIElement(element);
            }
        }

        public void ClearDraw()
        {
            if (gridEnable != null)
            {
                gridEnable.enabled = false;
                gridEnable.enabled = true;
            }

            foreach (UIElement element in Elements)
            {
                UIPoolee poolee = element.GetComponent<UIPoolee>();
                poolee.Return();
                poolee.gameObject.SetActive(false);
            }

            Elements.Clear();
        }
        [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
        private void AssignUIElement(MenuElement element)
        {
            UIElement uiElement = elementTypes[element.Type].Spawn(ElementGrid.transform, true).GetComponent<UIElement>();
            uiElement.AssignElement(element);

            Elements.Add(uiElement);
        }
    }
}
