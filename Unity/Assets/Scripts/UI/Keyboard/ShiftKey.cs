using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    public class ShiftKey : Key
    {
        public override void OnKeyPressed()
        {
            _keyboard.ShiftKeys();
        }
    }
}