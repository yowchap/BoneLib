using System;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public abstract class Element : MonoBehaviour
    {
        public DirtyProperty<Vector2> position;
        public DirtyProperty<Vector2> size;

        public Action elementAction;
        public Page parentPage;

        public void Start()
        {
        }

        public void Update()
        {
            position.OnDirty((t) =>
            {
                transform.position = t;
            });

            size.OnDirty((t) =>
            {
                transform.localScale = t;
            });
        }
    }
}