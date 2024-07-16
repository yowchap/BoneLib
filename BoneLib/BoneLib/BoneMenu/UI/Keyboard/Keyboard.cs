using System;
using System.Collections.Generic;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class Keyboard : MonoBehaviour
    {
        public Keyboard(IntPtr ptr) : base(ptr) { }

        public TMP_InputField InputField => _inputField;

        private StringElement _connectedElement;
        private GUIStringElement _connectedGUIElement;
        private TMP_InputField _inputField;
        private List<Key> _keys;
        private Button _closeButton;
        private Button _pasteButton;
        private Button _clearButton;

        private void Awake()
        {
            _keys = new List<Key>();
            _inputField = transform.GetChild(0).GetChild(1).GetComponent<TMP_InputField>();
            _closeButton = transform.GetChild(0).Find("Close").GetComponent<Button>();
            _pasteButton = transform.GetChild(0).Find("Paste").GetComponent<Button>();
            _clearButton = transform.GetChild(0).Find("Clear").GetComponent<Button>();

            _closeButton.onClick.AddListener(new Action(() => { gameObject.SetActive(false); }));
            _pasteButton.onClick.AddListener(new Action(() => { _inputField.text += System.Windows.Forms.Clipboard.GetText(); }));
            _clearButton.onClick.AddListener(new Action(() => { _inputField.text = string.Empty; }));
        }

        public void RegisterKey(Key key)
        {
            _keys.Add(key);
        }

        public void ShiftKeys()
        {
            for (int i = 0; i < _keys.Count; i++)
            {
                _keys[i].Shift();
            }
        }

        public void ConnectElement(GUIStringElement guiElement)
        {
            _connectedGUIElement = guiElement;
            _connectedElement = _connectedGUIElement.BackingElement;
            _inputField.text = _connectedElement.Value;
        }

        public void SubmitOutput()
        {
            if (_connectedElement == null)
            {
                throw new NullReferenceException("Connected element is not connected, or is null!");
            }

            _connectedElement.Value = _inputField.text;
            _connectedElement.OnElementSelected();
            _connectedGUIElement.Draw();
        }
    }
}