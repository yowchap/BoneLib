using System;
using System.Collections.Generic;
using UnityEngine;

namespace BoneLib.BoneMenu.Elements
{
    public class ListElement<T> : GenericElement<T> where T : class
    {
        public ListElement(string name, Color color, List<T> list = null, Action<T> action = null) : base(name, color, action)
        {
            Name = name;
            Color = color;
            this._action = action;

            _elements = list != null ? list : new List<T>();
        }

        public override string Type => ElementType.Toggle;
        public T this[int i] { get => _elements[i]; }

        protected List<T> _elements;

        public void Add(T type)
        {
            _elements?.Add(type);
        }

        public void Remove(T type)
        {
            _elements?.Remove(type);
        }

        public void Select(T value)
        {
            this._value = value;
            OnChangedValue();
        }

        public List<T> GetElements()
        {
            return _elements;
        }
    }
}
