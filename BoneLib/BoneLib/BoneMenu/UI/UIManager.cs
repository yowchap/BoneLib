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

        public UIPage MainPage;

        private GameObject pagePool;
        private GameObject categoryPool;
        private GameObject functionPool;
        private GameObject valuePool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(Instance.gameObject);
            }

            SetupPools();

            MainPage = PagePool.Spawn(transform).GetComponent<UIPage>();
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
            if (category == null)
            {
                return;
            }
        }

        public void OnCategorySelected(MenuCategory category)
        {
            if (category == null)
            {
                return;
            }

            MainPage.AssignElement(category);
            MainPage.Draw();
            MainPage.gameObject.SetActive(true);
        }

        private void SetupPools()
        {
            pagePool = new GameObject("Page Pool");
            categoryPool = new GameObject("Category Pool");
            functionPool = new GameObject("Function Pool");
            valuePool = new GameObject("Value Pool");

            PagePool = pagePool?.AddComponent<UIPool>();
            CategoryPool = categoryPool?.AddComponent<UIPool>();
            FunctionPool = functionPool?.AddComponent<UIPool>();
            ValuePool = valuePool?.AddComponent<UIPool>();

            PagePool.SetCount(2);
            CategoryPool.SetCount(6);
            FunctionPool.SetCount(6);
            ValuePool.SetCount(6);

            PagePool.SetPrefab(DataManager.UI.PagePrefab);
            CategoryPool.SetPrefab(DataManager.UI.CategoryPrefab);
            FunctionPool.SetPrefab(DataManager.UI.FunctionPrefab);
            ValuePool.SetPrefab(DataManager.UI.ValuePrefab);

            PagePool.Populate(PagePool.Count);
            CategoryPool.Populate(CategoryPool.Count);
            FunctionPool.Populate(FunctionPool.Count);
            ValuePool.Populate(ValuePool.Count);
        }
    }
}
