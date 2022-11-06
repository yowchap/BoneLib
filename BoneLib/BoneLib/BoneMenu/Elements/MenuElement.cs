using UnityEngine;

using TMPro;

namespace BoneLib.BoneMenu
{
    public abstract class MenuElement
    {
        public MenuElement(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; protected set; }
        public Color Color { get; protected set; }

        public virtual string Type { get => ElementType.Type_Default; }

        protected TextMeshPro _nameText;
        protected TextMeshPro _valueText;

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }

        public void SetNameTextMesh(TextMeshPro nameText)
        {
            _nameText = nameText;
        }

        public void SetValueTextMesh(TextMeshPro valueText)
        {
            _valueText = valueText;
        }

        public virtual void OnSelectElement() { }
        public virtual void OnSelectLeft() { }
        public virtual void OnSelectRight() { }

        protected void UpdateText(TextMeshPro tmPro, float value)
        {
            UpdateText(tmPro, value.ToString());
        }

        protected void UpdateText(TextMeshPro tmPro, int value)
        {
            UpdateText(tmPro, value.ToString());
        }

        protected void UpdateText(TextMeshPro tmPro, string text)
        {
            if(tmPro == null)
            {
                return;
            }

            tmPro.text = text;
        }
    }
}
