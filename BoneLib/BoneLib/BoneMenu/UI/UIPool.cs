using UnityEngine;

using System.Collections.Generic;
using System.Linq;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPool : MonoBehaviour
    {
        public UIPool(System.IntPtr ptr) : base(ptr) { }

        public List<GameObject> Pool { get => _pool; }
        public GameObject Prefab { get => _prefab; }
        public int Count { get => _count; }

        private GameObject _prefab;
        private List<GameObject> _pool;
        private int _count;

        private void Awake()
        {
            name = $"[Pool] - {_prefab.name}";
            _pool = new List<GameObject>();
        }

        public void SetCount(int count)
        {
            _count = count;
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
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
                prefab.AddComponent<UIPoolee>().SetParent(this);
                prefab.SetActive(false);
                _pool.Add(prefab);
            }
        }

        public GameObject Enable(Transform parent)
        {
            var selected = _pool.First();
            selected.transform.SetParent(parent);
            selected.SetActive(true);
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
