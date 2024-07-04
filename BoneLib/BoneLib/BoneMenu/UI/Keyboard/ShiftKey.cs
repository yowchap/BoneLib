namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class ShiftKey : Key
    {
        public ShiftKey(System.IntPtr ptr) : base(ptr) { }

        public override void OnKeyPressed()
        {
            _keyboard.ShiftKeys();
        }
    }
}