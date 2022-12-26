using MelonLoader;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPool : MonoBehaviour
    {
        public UIPool(System.IntPtr ptr) : base(ptr) { }

        [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
        public List<UIPoolee> Pool { get => _pool; }
        [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
        public List<UIPoolee> Active { get => _active; }
        [UnhollowerBaseLib.Attributes.HideFromIl2Cpp]
        public List<UIPoolee> Inactive { get => _inactive; }

        public GameObject Prefab { get => _prefab; }
        public int Count { get => _count; }

        private GameObject _prefab;

        private List<UIPoolee> _pool = new List<UIPoolee>();
        private List<UIPoolee> _active = new List<UIPoolee>();
        private List<UIPoolee> _inactive = new List<UIPoolee>();

        private int _count;

        private void Start()
        {
            name = $"[Pool] - {_prefab.name}";
        }

        public void SetCount(int count)
        {
            _count = count;
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
            _prefab.hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        public void Populate(int byElements)
        {
            if (_prefab == null)
            {
                return;
            }

            for (int i = 0; i < byElements; i++)
            {
                GameObject prefab = CreatePrefab(_prefab);
                UIPoolee poolee = prefab.AddComponent<UIPoolee>();

                poolee.SetParent(this);
                prefab.SetActive(false);

                _pool.Add(poolee);
                _inactive.Add(poolee);
            }
        }

        public UIPoolee Spawn(Transform parent, bool startActive = false)
        {
            UIPoolee selected = GetInactive();

            if (selected == null)
            {
                Populate(2);
                return Spawn(parent, startActive);
            }

            if (HelperMethods.IsAndroid())
                MelonCoroutines.Start(AttemptParent(selected, parent));
            else
            {
                selected.transform.SetParent(parent);
                selected.transform.localPosition = Vector3.zero;
                selected.transform.rotation = parent.rotation;
            }

            selected.gameObject.SetActive(startActive);

            _active.Add(selected);
            _inactive.Remove(selected);

            return selected;
        }

        public UIPoolee Spawn(Transform parent, int orderInHierarchy, bool startActive = false)
        {
            UIPoolee selected = GetInactive();

            if (selected == null)
            {
                Populate(2);
                return Spawn(parent, startActive);
            }

            if (HelperMethods.IsAndroid())
                MelonCoroutines.Start(AttemptParent(selected, parent, false));
            else
                selected.transform.SetParent(parent);

            selected.transform.SetSiblingIndex(orderInHierarchy);
            selected.gameObject.SetActive(startActive);

            _active.Add(selected);
            _inactive.Remove(selected);

            return selected;
        }

        private GameObject CreatePrefab(GameObject prefab)
        {
            GameObject _object = GameObject.Instantiate(prefab, transform);
            _object.SetActive(true);
            return _object;
        }

        private UIPoolee GetInactive()
        {
            return _inactive.FirstOrDefault();
        }

        private System.Collections.IEnumerator AttemptParent(UIPoolee selected, Transform parent, bool setPosRot = true)
        {
            int run = 0;
            bool parentSet = false;
            while (!parentSet)
            {
                run++;
                selected.transform.SetParent(parent);
                parentSet = selected.transform.parent == parent;

                if (setPosRot && parentSet)
                {
                    selected.transform.localPosition = Vector3.zero;
                    selected.transform.rotation = parent.rotation;
                }

                if (run >= 10)
                    yield return null;
            }
        }
    }
}
