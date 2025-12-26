namespace BoneLib.BoneMenu.UI
{
    public class EnterKey : Key
    {
        public override void OnKeyPressed()
        {
            _keyboard.SubmitOutput();
        }
    }
}