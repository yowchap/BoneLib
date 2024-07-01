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

            Value = Shifted ? KeyName.ToUpper() : KeyName.ToLower();
        }    

        public virtual void OnKeyPressed()
        {
            print("Key: " + KeyName + "\nValue: " + Value + "\nShifted: " + Shifted);
            _keyboard.InputField.text += Value;
        }

        public void Shift()
        {
            Shifted = !Shifted;
            Value = Shifted ? KeyName.ToUpper() : KeyName.ToLower();

            // this key has a logo instead of a character
            if (_textRepresenter != null)
            {
                _textRepresenter.text = Value;
            }
        }
    }
}