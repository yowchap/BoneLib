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
        }

        private void Start()
        {

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

            MainPage = PagePool.Enable(transform).GetComponent<UIPage>();
            MainPage.Draw();
        }

        private void SetupPools()
        {
            PagePool = pagePool?.GetComponent<UIPool>();
            CategoryPool = categoryPool?.GetComponent<UIPool>();
            FunctionPool = functionPool?.GetComponent<UIPool>();
            ValuePool = valuePool?.GetComponent<UIPool>();

            PagePool.SetCount(2);
            CategoryPool.SetCount(2);
            FunctionPool.SetCount(2);
            ValuePool.SetCount(2);

            PagePool.SetPrefab(MenuManager.UI.PagePrefab);
            CategoryPool.SetPrefab(MenuManager.UI.CategoryPrefab);
            FunctionPool.SetPrefab(MenuManager.UI.FunctionPrefab);
            ValuePool.SetPrefab(MenuManager.UI.ValuePrefab);

            PagePool.Populate(PagePool.Count);
            CategoryPool.Populate(CategoryPool.Count);
            FunctionPool.Populate(FunctionPool.Count);
            ValuePool.Populate(ValuePool.Count);
        }
    }
}
