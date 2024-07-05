using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Key : MonoBehaviour
    {
        public Key(System.IntPtr ptr) : base(ptr) { }

        public string KeyName { get; private set; }
        public string Value { get; private set; }
        public bool Shifted { get; private set; } = false;
        
        protected Keyboard _keyboard;

        private TextMeshProUGUI _textRepresenter;
        private Button _keyButton;

        protected void Start()
        {
            KeyName = name;

            _keyboard = GetComponentInParent<Keyboard>();
            _keyButton = GetComponent<Button>();
            _textRepresenter = transform.GetComponentInChildren<TextMeshProUGUI>();
            _keyButton.onClick.AddListener(new System.Action(() => { OnKeyPressed(); }));
            _keyboard.RegisterKey(this);

            if (KeyName.Length < 3)
            {
                Value = $"{KeyName[0]}";
                return;
            }

            if (KeyName[1] != ':')
            {
                Value = $"{KeyName[0]}";
                return;
            }

            Value = Shifted ? $"{KeyName[2]}" : $"{KeyName[0]}";
            _textRepresenter.text = Value;
        }    

        public virtual void OnKeyPressed()
        {
            _keyboard.InputField.text += Value;
        }

        public void Shift()
        {
            if (KeyName.Length < 3)
            {
                return;
            }

            if (KeyName[1] != ':')
            {
                return;
            }

            Shifted = !Shifted;
            Value = Shifted ? $"{KeyName[2]}" : $"{KeyName[0]}";
            _textRepresenter.text = Value;
        }
    }
}