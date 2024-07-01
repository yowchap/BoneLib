namespace BoneLib.BoneMenu.UI
{
    public class EnterKey : Key
    {
        public EnterKey(System.IntPtr ptr) : base(ptr) { }

        public override void OnKeyPressed()
        {
            _keyboard.SubmitOutput();
        }
    }
}