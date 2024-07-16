using UnityEngine;
using UnityEngine.UI;
using Il2CppTMPro;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GUIBoolElement : GUIElement
    {
        public GUIBoolElement(System.IntPtr ptr) : base(ptr) { }

        public BoolElement BackingElement => _backingElement;

        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _valueText;
        private Button _button;

        private BoolElement _backingElement;

        private void Awake()
        {
            _nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            _valueText = transform.Find("Button/Value").GetComponent<TextMeshProUGUI>();
            _button = transform.Find("Button").GetComponent<Button>();

            _button.onClick.AddListener(new System.Action(() => OnPressed()));
        }

        public void AssignElement(BoolElement element)
        {
            _backingElement = element;
            element.OnElementChanged += Draw;
        }

        public override void Draw()
        {
            base.Draw();

            _nameText.text = _backingElement.ElementName;
            _nameText.color = _backingElement.ElementColor;

            _valueText.text = _backingElement.Value ? "Enabled" : "Disabled";
        }

        public override void OnPressed()
        {
            _backingElement.OnElementSelected();
            Draw();
        }
    }
}