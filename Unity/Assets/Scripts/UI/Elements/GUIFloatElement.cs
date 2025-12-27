using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace BoneLib.BoneMenu.UI
{
    public class GUIFloatElement : GUIElement
    {
        public FloatElement BackingElement => _backingElement;

        private Button _decrement;
        private Button _increment;
        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _valueText;

        private FloatElement _backingElement;

        private void Awake()
        {
            _nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            _valueText = transform.Find("Value").GetComponent<TextMeshProUGUI>();

            _decrement = transform.Find("Decrement").GetComponent<Button>();
            _increment = transform.Find("Increment").GetComponent<Button>();

            _infoButton = transform.Find("Tooltip").GetComponent<Button>();

            _decrement.onClick.AddListener(() => OnDecrement());
            _increment.onClick.AddListener(() => OnIncrement());
            _infoButton.onClick.AddListener(() => Menu.DisplayDialog("INFO", _backingElement.ElementTooltip));
            _infoButton.gameObject.SetActive(false);
        }

        public void AssignElement(FloatElement element)
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

            _valueText.text = _backingElement.Value.ToString("0.####", CultureInfo.InvariantCulture);

            _infoButton.gameObject.SetActive(_backingElement.HasTooltip);
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