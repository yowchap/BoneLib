using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GUIDialog : MonoBehaviour
    {
        public GUIDialog(System.IntPtr ptr) : base(ptr) { }

        private Dialog _dialog;

        private TextMeshProUGUI _titleText;
        private TextMeshProUGUI _descriptionText;

        private Button _confirmButton;
        private Button _cancelButton;
        private Button _closeButton;

        private void Awake()
        {
            _titleText = transform.Find("Header/Title").GetComponent<TextMeshProUGUI>();
            _descriptionText = transform.Find("Container/Description").GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _confirmButton?.onClick.AddListener(new System.Action(() => _dialog.OnConfirmPressed()));
            _cancelButton?.onClick.AddListener(new System.Action(() => _dialog.OnDeclinePressed()));
            _closeButton?.onClick.AddListener(new System.Action(() => gameObject.SetActive(false)));

            Draw();
        }

        private void OnDisable()
        {
            _confirmButton?.onClick.RemoveListener(new System.Action(() => _dialog.OnConfirmPressed()));
            _cancelButton?.onClick.RemoveListener(new System.Action(() => _dialog.OnDeclinePressed()));
            _closeButton?.onClick.RemoveListener(new System.Action(() => gameObject.SetActive(false)));
        }

        public void AssignDialog(Dialog dialog)
        {
            _dialog = dialog;
        }

        public void Draw()
        {
            _titleText.text = _dialog.DialogTitle;
            _descriptionText.text = _dialog.DialogDescription;

            gameObject.SetActive(true);
        }
    }
}