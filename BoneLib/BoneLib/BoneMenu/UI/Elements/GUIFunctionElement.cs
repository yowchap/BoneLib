using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GUIFunctionElement : GUIElement
    {
        public GUIFunctionElement(System.IntPtr ptr) : base(ptr) { }

        public FunctionElement BackingElement => _backingElement;

        private Button _button;
        private TextMeshProUGUI _nameText;
        private RawImage _logo;
        private GameObject _backline;

        private FunctionElement _backingElement;

        private AspectRatioFitter _fitter;

        private void Awake()
        {
            _nameText = transform.Find("Button/Name").GetComponent<TextMeshProUGUI>();
            _logo = transform.Find("Button/Logo").GetComponent<RawImage>();
            _button = transform.Find("Button").GetComponent<Button>();
            _backline = transform.Find("Button/Backline").gameObject;
            _fitter = GetComponentInChildren<AspectRatioFitter>(true);
            _button.onClick.AddListener(new System.Action(() => OnPressed()));
        }

        public void AssignElement(FunctionElement element)
        {
            _backingElement = element;
            element.OnElementChanged += Draw;
        }

        public override void OnPressed()
        {
            if (_backingElement == null)
            {
                return;
            }

            _backingElement.OnElementSelected();
        }

        public override void Draw()
        {
            base.Draw();

            _backline.SetActive(!_backingElement.Properties.HasFlag(ElementProperties.NoBorder));
            
            if (_nameText == null)
            {
                return;
            }

            _nameText.text = _backingElement.ElementName;
            _logo.texture = _backingElement.Logo;

            _nameText.gameObject.SetActive(_logo.texture == null);
            _logo.gameObject.SetActive(_logo.texture != null);

            if (_logo.texture != null)
            {
                _logo.SetNativeSize();
                float width = _logo.texture.width;
                float height = _logo.texture.height;

                _fitter.aspectRatio = width / height;
            }
        }
    }
}
