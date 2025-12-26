using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    public class GUIIntElement : GUIElement
    {
        public IntElement BackingElement => _backingElement;

        private Button _decrement;
        private Button _increment;

        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _valueText;

        private IntElement _backingElement;

        private void Awake()
        {
            _nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            _valueText = transform.Find("Value").GetComponent<TextMeshProUGUI>();

            _decrement = transform.Find("Decrement").GetComponent<Button>();
            _increment = transform.Find("Increment").GetComponent<Button>();

            _decrement.onClick.AddListener(() => OnDecrement());
            _increment.onClick.AddListener(() => OnIncrement());
        }

        public void AssignElement(IntElement element)
        {
            _backingElement = element;
            element.OnElementChanged += Refresh;
        }

        private void OnDestroy()
        {
            if (_backingElement != null)
            {
                _backingElement.OnElementChanged -= Refresh;
            }
        }

        public override void Draw()
        {
            base.Draw();

            Refresh();
        }

        public void Refresh()
        {
            if (_nameText == null)
            {
                return;
            }

            _nameText.text = _backingElement.ElementName;
            _nameText.color = _backingElement.ElementColor;

            _valueText.text = _backingElement.Value.ToString();
        }

        public void OnIncrement()
        {
            _backingElement.Increment();
            Refresh();
        }

        public void OnDecrement()
        {
            _backingElement.Decrement();
            Refresh();
        }
    }
}