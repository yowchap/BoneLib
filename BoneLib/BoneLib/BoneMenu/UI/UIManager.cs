using UnityEngine;

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
    }
}
