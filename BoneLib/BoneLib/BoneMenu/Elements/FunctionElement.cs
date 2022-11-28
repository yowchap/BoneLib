using System;

using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class FunctionElement : MenuElement
    {
        public FunctionElement(string name, Color color, Action action) : base(name, color)
        {
            Name = name;
            Color = color;
            Action = action;
            _confirmer = false;
        }

        public FunctionElement(string name, Color color, Action action, string confirmText = "") : base(name, color)
        {
            Name = name;
            Color = color;
            Action = action;
            _confirmText = confirmText;
            _confirmer = true;
        }

        public string ConfirmText { get => _confirmText; }

        public override ElementType Type => ElementType.Function;

        public Action Action { get; private set; }
        public bool Confirmer { get => _confirmer; }

        private string _confirmText;
        private bool _confirmer;

        public override void OnSelectElement()
        {
            if (_confirmer)
            {
                OnSelectConfirm();
            }
            else
            {
                Action?.Invoke();
            }
        }

        public void OnSelectConfirm()
        {
            Action?.Invoke();
        }
    }
}
