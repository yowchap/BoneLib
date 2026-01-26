using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    public class Key : MonoBehaviour
    {
        public string KeyName { get; private set; }
        public string Value { get; private set; }
        public bool Shifted { get; private set; } = false;

        protected Keyboard _keyboard;

        private TextMeshProUGUI _textRepresenter;
        private Button _keyButton;

        protected void Start()
        {
            KeyName = name;

            _keyboard = GetComponentInParent<Keyboard>(true);
            _keyButton = GetComponent<Button>();
            _textRepresenter = transform.GetComponentInChildren<TextMeshProUGUI>();
            _keyButton.onClick.AddListener(OnKeyPressed);
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