using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    public class SpaceKey : Key
    {
        public SpaceKey(System.IntPtr ptr) : base(ptr) { }

        public override void OnKeyPressed()
        {
            _keyboard.InputField.text += " ";
        }
    }
}