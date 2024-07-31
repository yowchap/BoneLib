using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class Dialog
    {
        public Dialog(string title, string description, Texture2D icon, Action confirmAction = null, Action denyAction = null)
        {
            _dialogTitle = title;
            _dialogDescription = description;
            _dialogIcon = icon;

            _confirmAction = confirmAction;
            _denyAction = denyAction;
        }

        public static Action<Dialog> OnDialogOpened;
        public static Action<Dialog> OnDialogClosed;

        public static Texture2D ErrorIcon;
        public static Texture2D WarningIcon;
        public static Texture2D InfoIcon;
        public static Texture2D QuestionIcon;

        public string DialogTitle => _dialogTitle;
        public string DialogDescription => _dialogDescription;
        public bool HasConfirmAction => _confirmAction != null;
        public bool HasDenyAction => _denyAction != null;
        public Texture2D DialogIcon => _dialogIcon;

        private Action _confirmAction;
        private Action _denyAction;

        private string _dialogTitle;
        private string _dialogDescription;
        private Texture2D _dialogIcon;

        public void OnConfirmPressed()
        {
            _confirmAction.InvokeActionSafe();
        }

        public void OnDeclinePressed()
        {
            _denyAction.InvokeActionSafe();
        }
    }
}