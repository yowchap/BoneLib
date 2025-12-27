using UnityEngine.UI;
using Il2CppTMPro;
using Il2CppInterop.Runtime.Attributes;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public sealed class GUIStringElement : GUIElement
    {
        public GUIStringElement(System.IntPtr ptr) : base(ptr) { }
        [HideFromIl2Cpp]
        public StringElement BackingElement => _backingElement;

        private TMP_InputField _inputField;
        private TextMeshProUGUI _nameText;
        private Button _keyboardButton;

        private StringElement _backingElement;

        private void Awake()
        {
            _nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
            _inputField = GetComponent<TMP_InputField>();
            _keyboardButton = transform.Find("ShowKeyboard").GetComponent<Button>();
            _infoButton = transform.Find("Tooltip").GetComponent<Button>();

            _keyboardButton.onClick.AddListener(new System.Action(() => OpenKeyboard()));
            _inputField.onSubmit.AddListener(new System.Action<string>((str) => OnInputFieldSubmit(str)));
            _infoButton.onClick.AddListener(new System.Action(() => Menu.DisplayDialog("INFO", _backingElement.ElementTooltip)));
            _infoButton.gameObject.SetActive(false);
        }

        [HideFromIl2Cpp]
        public void AssignElement(StringElement element)
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

            _inputField.text = _backingElement.Value;

            _infoButton.gameObject.SetActive(_backingElement.HasTooltip);
        }

        private void OnInputFieldSubmit(string input)
        {
            _inputField.text = input;
            _backingElement.Value = input;
            Refresh();
        }

        private void OpenKeyboard()
        {
            GUIMenu.Instance.OpenKeyboard();
            GUIMenu.Instance.ConnectElementToKeyboard(this);
        }
    }
}