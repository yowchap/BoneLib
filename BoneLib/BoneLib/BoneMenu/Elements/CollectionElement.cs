using UnityEngine;

using System;
using System.Collections.Generic;

namespace BoneLib.BoneMenu
{
    public class CollectionElement<T> : GenericElement<T> where T : UnityEngine.Object
    {
        public CollectionElement(string name, Color color, T[] objects) : base(name, color)
        {
            Name = name;
            Color = color;
            this.objects = objects;
        }

        private T[] objects;

        public override string Type => ElementType.Type_Value;
        public override string DisplayValue => value.name.ToString();

        public override void OnSelectLeft()
        {
            value = (T)GetNextValue();
        }

        public override void OnSelectRight()
        {
            value = (T)GetPreviousValue();
        }

        // from MTINM.BoneMenu
        private UnityEngine.Object GetNextValue()
        {
            Array values = objects;
            int nextIndex = Array.IndexOf(values, value) + 1;
            return nextIndex == values.Length ? (UnityEngine.Object)values.GetValue(0) : (UnityEngine.Object)values.GetValue(nextIndex);
        }

        private UnityEngine.Object GetPreviousValue()
        {
            Array values = objects;
            int previousIndex = Array.IndexOf(values, value) - 1;
            return previousIndex > -1 ? (UnityEngine.Object)values.GetValue(previousIndex) : (UnityEngine.Object)values.GetValue(values.Length - 1);
        }
    }
}
