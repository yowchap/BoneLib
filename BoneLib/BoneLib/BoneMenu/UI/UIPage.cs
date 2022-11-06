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

        private void Awake()
        {
            _elementGrid = transform.Find("Viewport/ElementGrid");
        }

        public void AddElement()
        {

        }
    }
}
