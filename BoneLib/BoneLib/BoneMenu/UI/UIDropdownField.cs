using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIDropdownField : UIElement
    {
        public UIDropdownField(IntPtr ptr) : base(ptr) { }

        public override string Type => ElementType.Toggle;

        private TMPro.TMP_Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = transform.Find("Dropdown").GetComponent<TMPro.TMP_Dropdown>();
            _dropdown.options.Clear();
            SetupListeners();
        }

        private void Start()
        {
            ListElement<string> list1;
            CheckType(out list1);
        }

        private void SetupListeners()
        {
        }

        private bool CheckType<T>(out ListElement<T> listElement) where T : class
        {
            if((ListElement<T>)_element == null)
            {
                listElement = null;
                return false;
            }

            var casted = (ListElement<T>)_element;
            listElement = casted;
            return casted != null;
        }
    }
}
