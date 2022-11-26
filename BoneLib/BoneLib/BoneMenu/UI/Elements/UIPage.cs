using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

using SLZ.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPage : UIElement
    {
        public UIPage(IntPtr ptr) : base(ptr) { }

        public List<UIElement> Elements { get; private set; } = new List<UIElement>();
        public Transform ElementGrid { get; private set; }

        private SLZ.UI.UIGridEnable gridEnable;

        private Transform returnArrow;

        private Button returnButton;

        private ButtonHoverClick returnFeedback;

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

            gridEnable = transform.Find("Viewport/ElementGrid").GetComponent<SLZ.UI.UIGridEnable>();
            returnButton = returnArrow.GetComponent<Button>();

            returnFeedback = returnButton.GetComponent<ButtonHoverClick>();

            returnFeedback.feedback_audio = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackAudio;
            returnFeedback.feedback_tactile = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackTactile;
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

            if (gridEnable != null)
            {
                gridEnable.enabled = true;
            }

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
            if(gridEnable != null)
            {
                gridEnable.enabled = false;
                gridEnable.enabled = true;
            }

            foreach (var element in Elements)
            {
                var poolee = element.GetComponent<UIPoolee>();
                poolee.Return();
                poolee.gameObject.SetActive(false);
            }

            Elements.Clear();
        }

        private void AssignUIElement(MenuElement element)
        {
            var uiElement = elementTypes[element.Type].Spawn(ElementGrid.transform, true).GetComponent<UIElement>();
            uiElement.AssignElement(element);

            Elements?.Add(uiElement);
        }
    }
}
