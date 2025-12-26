using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    public class DoubleZeroKey : Key
    {
        public override void OnKeyPressed()
        {
            _keyboard.InputField.text += "00";
        }
    }
}
