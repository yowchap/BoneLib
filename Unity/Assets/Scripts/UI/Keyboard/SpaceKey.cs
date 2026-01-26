using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    public class SpaceKey : Key
    {
        public override void OnKeyPressed()
        {
            _keyboard.InputField.text += " ";
        }
    }
}