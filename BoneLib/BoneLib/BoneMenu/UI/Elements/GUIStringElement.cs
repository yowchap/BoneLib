using UnityEngine;
using UnityEngine.UI;
using Il2CppTMPro;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GUIStringElement : GUIElement
    {
        public GUIStringElement(System.IntPtr ptr) : base(ptr) { }

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

            _keyboardButton.onClick.AddListener(new System.Action(() => OpenKeyboard()));
            _inputField.onSubmit.AddListener(new System.Action<string>((str) => OnInputFieldSubmit(str)));
        }

        public void AssignElement(StringElement element)
        {
            _backingElement = element;
            element.OnElementChanged += Draw;
        }

        public override void Draw()
        {
            base.Draw();

            _nameText.text = _backingElement.ElementName;
            _inputField.text = _backingElement.Value;
        }

        private void OnInputFieldSubmit(string input)
        {
            _inputField.text = input;
            _backingElement.Value = input;
            Draw();
        }

        private void OpenKeyboard()
        {
            GUIMenu.Instance.OpenKeyboard();
            GUIMenu.Instance.ConnectElementToKeyboard(this);
        }
    }
}