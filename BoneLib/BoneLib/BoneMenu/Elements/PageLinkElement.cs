using Il2CppInterop.Runtime.Attributes;
using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class PageLinkElement : FunctionElement
    {
        public PageLinkElement(string name, Color color, Action callback) : base(name, color, callback)
        {
        }

        private Page _linkedPage;

        public Page LinkedPage => _linkedPage;

        public void AssignPage(Page page)
        {
            _linkedPage = page;

            Menu.OnPageUpdated += OnPageUpdated;
        }

        public override void OnElementRemoved()
        {
            base.OnElementRemoved();

            if (_linkedPage != null)
            {
                Menu.OnPageUpdated -= OnPageUpdated;

                _linkedPage = null;
            }
        }
        [HideFromIl2Cpp]
        private void OnPageUpdated(Page page)
        {
            if (page != _linkedPage)
            {
                return;
            }

            ElementName = page.Name;
            ElementColor = page.Color;
        }
    }
}