using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoneLib.UI
{
    public abstract class Element
    {
        public DirtyProperty<Vector2> Position;
        public DirtyProperty<Vector2> Size;
        public DirtyProperty<Color> FrontColor;

        public Action ElementAction;

        public Element()
        {
            Position.OnDirty((t) => SetDirty(true));
            Size.OnDirty((t) => SetDirty(true));
            FrontColor.OnDirty((t) => SetDirty(true));
        }

        public bool IsDirty { get; private set; }

        public void SetDirty(bool dirty) => IsDirty = dirty;
    }
}