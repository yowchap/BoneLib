using UnityEngine;

using System.Collections.Generic;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIPool : MonoBehaviour
    {
        public UIPool(System.IntPtr ptr) : base(ptr) { }

        public List<GameObject> Pool { get => _pool; }
        public GameObject Prefab { get => _prefab; }
        public int Count { get => _count; }

        private List<GameObject> _pool;

        private GameObject _prefab;
        private int _count;

        private void Awake()
        {
            name = $"[Pool] - {_prefab.name}";
        }

        private void Start()
        {
            _pool = new List<GameObject>();
            transform.parent = UIManager.Instance.transform;
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
            if(_prefab == null)
            {
                return;
            }

            for(int i = 0; i < byElements; i++)
            {
                _pool.Add(CreatePrefab(_prefab));
            }
        }

        private GameObject CreatePrefab(GameObject prefab)
        {
            var _object = GameObject.Instantiate(prefab, transform);
            _object.SetActive(false);
            return _object;
        }
    }
}
