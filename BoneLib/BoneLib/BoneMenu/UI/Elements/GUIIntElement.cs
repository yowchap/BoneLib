using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GUIIntElement : GUIElement
    {
        public GUIIntElement(System.IntPtr ptr) : base(ptr) { }

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

            _decrement.onClick.AddListener(new System.Action(() => OnDecrement()));
            _increment.onClick.AddListener(new System.Action(() => OnIncrement()));
        }

        public void AssignElement(IntElement element)
        {
            _backingElement = element;
            element.OnElementChanged += Draw;
        }

        public override void Draw()
        {
            base.Draw();

            _nameText.text = _backingElement.ElementName;
            _valueText.text = _backingElement.Value.ToString();
        }

        public void OnIncrement()
        {
            _backingElement.Increment();
            Draw();
        }

        public void OnDecrement()
        {
            _backingElement.Decrement();
            Draw();
        }
    }
}