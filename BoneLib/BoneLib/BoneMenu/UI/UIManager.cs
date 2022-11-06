using UnityEngine;

using System.Collections.Generic;

namespace BoneLib.BoneMenu.UI
{
    /// <summary>
    /// The UI manager's purpose is to tie the internal menu together with the UI.
    /// It can show/hide pages, go back to pages, and add/remove elements.
    /// </summary>
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIManager : MonoBehaviour
    {
        public UIManager(System.IntPtr ptr) : base(ptr) { }

        public static UIManager Instance { get; private set; }

        public UIPool PagePool { get; private set; }
        public UIPool CategoryPool { get; private set; }
        public UIPool FunctionPool { get; private set; }
        public UIPool ValuePool { get; private set; }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance.gameObject);
            }

            SetupPools();
        }

        private void OnEnable()
        {
            MenuManager.OnCategoryCreated += OnCategoryCreated;
            MenuManager.OnCategorySelected += OnCategorySelected;
        }

        private void OnDisable()
        {
            MenuManager.OnCategoryCreated -= OnCategoryCreated;
            MenuManager.OnCategorySelected -= OnCategorySelected;
        }

        public void OnCategoryCreated(MenuCategory category)
        {
            var categoryObject = GameObject.Instantiate(MenuManager.UI.CategoryFieldObject);
        }

        public void OnCategorySelected(MenuCategory category)
        {

        }

        private void SetupPools()
        {
            GameObject pagePool = new GameObject("Pool");
            GameObject categoryPool = new GameObject("Pool");
            GameObject functionPool = new GameObject("Pool");
            GameObject valuePool = new GameObject("Pool");

            PagePool = pagePool.AddComponent<UIPool>();
            CategoryPool = categoryPool.AddComponent<UIPool>();
            FunctionPool = functionPool.AddComponent<UIPool>();
            ValuePool = valuePool.AddComponent<UIPool>();

            PagePool.SetPrefab(MenuManager.UI.PageObject);
            CategoryPool.SetPrefab(MenuManager.UI.CategoryFieldObject);
            FunctionPool.SetPrefab(MenuManager.UI.FunctionFieldObject);
            ValuePool.SetPrefab(MenuManager.UI.NumberFieldObject);

            PagePool.SetCount(1);
            CategoryPool.SetCount(1);
            FunctionPool.SetCount(1);
            ValuePool.SetCount(1);

            PagePool.Populate(PagePool.Count);
            CategoryPool.Populate(PagePool.Count);
            FunctionPool.Populate(PagePool.Count);
            ValuePool.Populate(PagePool.Count);
        }
    }
}
