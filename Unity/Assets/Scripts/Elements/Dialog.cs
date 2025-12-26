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

        public static event Action<Dialog> OnDialogOpened;
        public static event Action<Dialog> OnDialogClosed;

        public static Texture2D ErrorIcon { get; internal set; }
        public static Texture2D WarningIcon { get; internal set; }
        public static Texture2D InfoIcon { get; internal set; }
        public static Texture2D QuestionIcon { get; internal set; }

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
            _confirmAction?.Invoke();
        }

        public void OnDeclinePressed()
        {
            _denyAction?.Invoke();
        }

        internal void Internal_OnDialogOpened()
        {
            OnDialogOpened?.Invoke(this);
        }

        internal void Internal_OnDialogClosed()
        {
            OnDialogClosed?.Invoke(this);
        }
    }
}