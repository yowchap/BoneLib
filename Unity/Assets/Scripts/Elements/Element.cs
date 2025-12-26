using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class Element
    {
        public Element(string name, Color color)
        {
            _elementName = name;
            _elementColor = color;
        }

        public string ElementName
        {
            get
            {
                return _elementName;
            }
            set
            {
                _elementName = value;
                OnElementChanged?.Invoke();
            }
        }

        public Color ElementColor
        {
            get
            {
                return _elementColor;
            }
            set
            {
                _elementColor = value;
                OnElementChanged?.Invoke();
            }
        }

        public string ElementTooltip
        {
            get
            {
                return _elementTooltip;
            }
            set
            {
                _elementTooltip = value;
                OnElementChanged?.Invoke();
            }
        }

        [System.Obsolete("ToolTips were planned to be added, but never finished and they do not do anything")]
        public bool HasTooltip => !string.IsNullOrEmpty(_elementTooltip);

        public ElementProperties Properties { get; private set; }

        public Action OnElementChanged;

        protected string _elementName;
        protected Color _elementColor;
        protected string _elementTooltip;

        public virtual void OnElementAdded()
        {
        }

        public virtual void OnElementHover()
        {
        }

        public virtual void OnElementSelected()
        {
        }

        public virtual void OnElementDeselected()
        {
        }

        public virtual void OnElementPressed()
        {
        }

        public virtual void OnElementRemoved()
        {
        }

        public void SetProperty(ElementProperties properties)
        {
            Properties = properties;
            OnElementChanged?.Invoke();
        }

        [System.Obsolete("ToolTips were planned to be added, but never finished and they do not do anything")]
        public void SetTooltip(string tooltip)
        {
            _elementTooltip = tooltip;
            OnElementChanged?.Invoke();
        }
    }
}