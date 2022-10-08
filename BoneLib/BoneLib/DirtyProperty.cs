using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BoneLib
{
    public class DirtyProperty<T>
    {
        public bool isDirty = true;
        protected T value;

        public DirtyProperty(T value)
        {
            this.value = value;
        }

        public delegate void DirtyHandler(T input);

        public void OnDirty(DirtyHandler handler)
        {
            if (isDirty)
                handler(this);
        }

        public static implicit operator DirtyProperty<T>(T value) // Setter
        {
            return new DirtyProperty<T>(value);
        }

        public static implicit operator T(DirtyProperty<T> prop) // Getter
        {
            prop.isDirty = false;
            return prop.value;
        }
    }
}