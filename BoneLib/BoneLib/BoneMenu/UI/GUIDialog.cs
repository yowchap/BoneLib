using Il2CppInterop.Runtime.Attributes;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public class GUIDialog : MonoBehaviour
    {
        public GUIDialog(System.IntPtr ptr) : base(ptr) { }

        private Dialog _dialog;

        private TextMeshProUGUI _titleText;
        private TextMeshProUGUI _descriptionText;

        private Button _acceptButton;
        private Button _denyButton;

        private void Awake()
        {
            _titleText = transform.Find("Header/Title").GetComponent<TextMeshProUGUI>();
            _descriptionText = transform.Find("Container/Description").GetComponent<TextMeshProUGUI>();

            _acceptButton = transform.Find("Container/ButtonGroup/Option1").GetComponent<Button>();
            _denyButton = transform.Find("Container/ButtonGroup/Option2").GetComponent<Button>();

            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _acceptButton?.onClick.AddListener(new System.Action(() => _dialog.OnConfirmPressed()));
            _denyButton?.onClick.AddListener(new System.Action(() => _dialog.OnDeclinePressed()));
        }

        private void OnDisable()
        {
            _acceptButton?.onClick.RemoveAllListeners();
            _denyButton?.onClick.RemoveAllListeners();
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

            gameObject.SetActive(true);

            _acceptButton.gameObject.SetActive(_dialog.DialogOptions.HasFlag(Dialog.Options.YesOption));
            _denyButton.gameObject.SetActive(_dialog.DialogOptions.HasFlag(Dialog.Options.NoOption));
        }
    }
}