using UnityEngine.UI;
using Il2CppTMPro;
using Il2CppInterop.Runtime.Attributes;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public sealed class GUIBoolElement : GUIElement
    {
        public GUIBoolElement(System.IntPtr ptr) : base(ptr) { }
        [HideFromIl2Cpp]
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
            _infoButton = transform.Find("Tooltip").GetComponent<Button>();

            _button.onClick.AddListener(new System.Action(() => OnPressed()));
            _infoButton.onClick.AddListener(new System.Action(() => Menu.DisplayDialog("INFO", _backingElement.ElementTooltip)));
            _infoButton.gameObject.SetActive(false);
        }
        [HideFromIl2Cpp]
        public void AssignElement(BoolElement element)
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

            _valueText.text = _backingElement.Value ? "Enabled" : "Disabled";

            _infoButton.gameObject.SetActive(_backingElement.HasTooltip);
        }

        public override void OnPressed()
        {
            _backingElement.OnElementSelected();
            Refresh();
        }
    }
}