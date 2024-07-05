namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class DoubleZeroKey : Key
    {
        public DoubleZeroKey(System.IntPtr ptr) : base(ptr) { }

        public override void OnKeyPressed()
        {
            _keyboard.InputField.text += "00";
        }
    }
}
