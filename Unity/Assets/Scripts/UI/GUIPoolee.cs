using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    public class GUIPoolee : MonoBehaviour
    {
        public static Dictionary<GameObject, GUIPoolee> Cache { get; private set; }

        private GUIPool _parent;

        private void Awake()
        {
            if (Cache == null)
            {
                Cache = new Dictionary<GameObject, GUIPoolee>();
            }
        }

        private void OnEnable()
        {
            Cache.Add(gameObject, this);
        }

        private void OnDisable()
        {
            Cache.Remove(gameObject);
        }

        public static GUIPoolee Get(GameObject go)
        {
            // not even gonna happen
            // bitch
            if (Cache == null)
            {
                return null;
            }

            if (Cache.TryGetValue(go, out GUIPoolee poolee))
            {
                return poolee;
            }

            return null;
        }

        public void SetParent(GUIPool parent)
        {
            _parent = parent;
        }

        public void Return()
        {
            _parent.OnReturn(this);
        }
    }
}

