using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class Dialog
    {
        public Dialog(string title, string description, Texture2D icon, Options options = Options.YesOption | Options.NoOption)
        {
            _dialogTitle = title;
            _dialogDescription = description;
            _dialogIcon = icon;
            _dialogOptions = options;
        }

        [System.Flags]
        public enum Options
        {
            YesOption = (1 << 0),
            NoOption = (1 << 1)
        }

        public static System.Action<Dialog> OnDialogCreated;
        public static System.Action<Dialog> OnDialogClosed;

        public static Texture2D ErrorIcon;
        public static Texture2D WarningIcon;
        public static Texture2D InfoIcon;
        public static Texture2D QuestionIcon;

        public string DialogTitle => _dialogTitle;
        public string DialogDescription => _dialogDescription;
        public Texture2D DialogIcon => _dialogIcon;
        public Options DialogOptions => _dialogOptions;
        public FunctionElement ConfirmFunction => _confirmFunction;
        public FunctionElement DeclineFunction => _declineFunction;

        private string _dialogTitle;
        private string _dialogDescription;
        private Texture2D _dialogIcon;
        private Options _dialogOptions;

        private FunctionElement _confirmFunction;
        private FunctionElement _declineFunction;

        public void OnConfirmPressed()
        {

        }

        public void OnDeclinePressed()
        {

        }
    }
}