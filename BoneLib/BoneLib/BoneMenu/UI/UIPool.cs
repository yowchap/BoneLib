using UnityEngine;

using System.Collections.Generic;
using System.Linq;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPool : MonoBehaviour
    {
        public UIPool(System.IntPtr ptr) : base(ptr) { }

        public List<UIPoolee> Pool { get => _pool; }

        public List<UIPoolee> Active { get => _active; }
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
                var prefab = CreatePrefab(_prefab);
                var poolee = prefab.AddComponent<UIPoolee>();

                poolee.SetParent(this);
                prefab.SetActive(false);

                _pool.Add(poolee);
                _inactive.Add(poolee);
            }
        }

        public UIPoolee Spawn(Transform parent, bool startActive = false)
        {
            var selected = _inactive.First();
            selected.transform.SetParent(parent);
            selected.gameObject.SetActive(startActive);

            selected.transform.localPosition = Vector3.zero;
            selected.transform.rotation = parent.rotation;

            _active.Add(selected);
            _inactive.Remove(selected);

            return selected;
        }

        private GameObject CreatePrefab(GameObject prefab)
        {
            var _object = GameObject.Instantiate(prefab, transform);
            _object.SetActive(true);
            return _object;
        }
    }
}
