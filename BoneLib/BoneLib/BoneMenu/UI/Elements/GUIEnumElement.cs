using Il2CppInterop.Runtime.Attributes;
using Il2CppTMPro;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public class GUIEnumElement : GUIElement
    {
        public GUIEnumElement(System.IntPtr ptr) : base(ptr) { }
        [HideFromIl2Cpp]
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
        [HideFromIl2Cpp]
        public void AssignElement(EnumElement element)
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

        public override void OnPressed()
        {
            _backingElement.GetNext();
            Draw();
        }
    }
}