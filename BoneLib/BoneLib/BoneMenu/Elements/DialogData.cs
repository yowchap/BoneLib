using System;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    /// <summary>
    /// Parameter structure for passing in to Menu.DisplayDialog.
    /// </summary>
    public class DialogData
    {
        public DialogData()
        {
            Title = "";
            Message = "";
            Primary = Dialog.DefaultPrimaryColor;
            Secondary = Dialog.DefaultSecondaryColor;
            Icon = null;
            Confirm = null;
            Deny = null;
        }

        /// <summary>
        /// The title of the dialog. Careful not to use too many characters!
        /// </summary>
        public string Title;

        /// <summary>
        /// The message of the dialog.
        /// </summary>
        public string Message;

        /// <summary>
        /// Background color.
        /// </summary>
        public Color Primary;

        /// <summary>
        /// Gradient color (header and background).
        /// </summary>
        public Color Secondary;

        /// <summary>
        /// Dialog icon that appears in the top-left corner.
        /// </summary>
        public Texture2D Icon;

        public Action Confirm;
        public Action Deny;
    }
}