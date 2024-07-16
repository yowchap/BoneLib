using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GUIEnumElement : GUIElement
    {
        public GUIEnumElement(System.IntPtr ptr) : base(ptr) { }

        public EnumElement BackingElement => _backingElement;

        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _valueText;
        private Button _button;

        private EnumElement _backingElement;

        private void Awake()
        {
            _nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            _valueText = transform.Find("Value").GetComponent<TextMeshProUGUI>();
            _button = transform.Find("Button").GetComponent<Button>();

            _button.onClick.AddListener(new System.Action(() => OnPressed()));
        }

        public void AssignElement(EnumElement element)
        {
            _backingElement = element;
            element.OnElementChanged += Draw;
        }

        private void OnDestroy()
        {
            if (_backingElement != null)
            {
                _backingElement.OnElementChanged -= Draw;
            }
        }

        public override void Draw()
        {
            base.Draw();
            _nameText.text = _backingElement.ElementName;
            _nameText.color = _backingElement.ElementColor;

            _valueText.text = _backingElement.Value.ToString();
        }

        public override void OnPressed()
        {
            _backingElement.GetNext();
            Draw();
        }
    }
}