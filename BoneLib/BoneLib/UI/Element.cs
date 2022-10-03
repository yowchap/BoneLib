using System;
using UnityEngine;

namespace BoneLib.UI
{
    public abstract class Element
    {
        public DirtyProperty<Vector2> position;
        public DirtyProperty<Vector2> size;
        public DirtyProperty<Color> frontColor;

        public Action elementAction;
        public Page parentPage;

        public Element()
        {
            position.OnDirty((t) => SetDirty(true));
            size.OnDirty((t) => SetDirty(true));
            frontColor.OnDirty((t) => SetDirty(true));
        }

        public bool IsDirty { get; private set; }

        public void SetDirty(bool dirty) => IsDirty = dirty;
    }
}