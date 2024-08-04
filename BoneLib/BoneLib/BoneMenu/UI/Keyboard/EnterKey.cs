namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public sealed class EnterKey : Key
    {
        public EnterKey(System.IntPtr ptr) : base(ptr) { }

        public override void OnKeyPressed()
        {
            _keyboard.SubmitOutput();
        }
    }
}