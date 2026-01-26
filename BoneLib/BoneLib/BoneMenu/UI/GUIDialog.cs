using Il2CppInterop.Runtime.Attributes;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public sealed class GUIDialog : MonoBehaviour
    {
        public GUIDialog(System.IntPtr ptr) : base(ptr) { }

        private Dialog _dialog;

        private TextMeshProUGUI _titleText;
        private TextMeshProUGUI _descriptionText;

        private Button _acceptButton;
        private Button _denyButton;
        private Button _closeButton;

        private Image _background;
        private Image _headerGradient;
        private Image _bodyGradient;

        private void Awake()
        {
            _titleText = transform.Find("Header/Title").GetComponent<TextMeshProUGUI>();
            _descriptionText = transform.Find("Container/Description").GetComponent<TextMeshProUGUI>();

            _acceptButton = transform.Find("Container/ButtonGroup/Option1").GetComponent<Button>();
            _denyButton = transform.Find("Container/ButtonGroup/Option2").GetComponent<Button>();
            _closeButton = transform.Find("Header/Toggle").GetComponent<Button>();

            _background = transform.Find("Background").GetComponent<Image>();
            _headerGradient = transform.Find("Background/HeaderGradient").GetComponent<Image>();
            _bodyGradient = transform.Find("Background/BodyGradient").GetComponent<Image>();

            _background.color = Dialog.DefaultPrimaryColor;
            _headerGradient.color = Dialog.DefaultSecondaryColor;
            _bodyGradient.color = Dialog.DefaultSecondaryColor;

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _acceptButton?.onClick.AddListener(new System.Action(() =>
            {
                _dialog.OnConfirmPressed();
                gameObject.SetActive(false);
                GUIMenu.Instance.ShowView(true);
            }));

            _denyButton?.onClick.AddListener(new System.Action(() =>
            {
                _dialog.OnDeclinePressed();
                gameObject.SetActive(false);
                GUIMenu.Instance.ShowView(true);
            }));

            _closeButton?.onClick.AddListener(new System.Action(() =>
            {
                _dialog.OnDeclinePressed();
                gameObject.SetActive(false);
                GUIMenu.Instance.ShowView(true);
            }));
        }

        private void OnDisable()
        {
            _acceptButton?.onClick.RemoveAllListeners();
            _denyButton?.onClick.RemoveAllListeners();
            _closeButton?.onClick.RemoveAllListeners();
        }

        [HideFromIl2Cpp]
        public void AssignDialog(Dialog dialog)
        {
            _dialog = dialog;
        }

        public void Draw()
        {
            _titleText.text = _dialog.DialogTitle;
            _descriptionText.text = _dialog.DialogDescription;

            _background.color = _dialog.PrimaryColor;
            _headerGradient.color = _dialog.SecondaryColor;
            _bodyGradient.color = _dialog.SecondaryColor;

            gameObject.SetActive(true);

            _acceptButton.gameObject.SetActive(_dialog.HasConfirmAction);
            _denyButton.gameObject.SetActive(_dialog.HasDenyAction);
        }
    }
}