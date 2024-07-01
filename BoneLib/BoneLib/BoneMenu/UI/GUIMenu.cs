using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class GUIMenu : MonoBehaviour
    {
        public GUIMenu(System.IntPtr ptr) : base(ptr) { }

        public static GUIMenu Instance { get; private set; }

        public Transform ActiveView { get; private set; }

        private GUIElementDrawer _drawer;
        private TextMeshProUGUI _headerText;
        private RawImage _headerLogo;
        private AspectRatioFitter _headerFitter;
        private TextMeshProUGUI _pageIndexText;
        private RawImage _background;
        private Button _decrementPageButton;
        private Button _incrementPageButton;
        private Button _toParentButton;
        private ScrollRect _scrollRect;
        private GUIDialog _guiDialog;
        private HorizontalLayoutGroup _horizontalLayoutGroup;
        private VerticalLayoutGroup _verticalLayoutGroup;
        private GridLayoutGroup _gridLayoutGroup;
        private Keyboard _keyboard;

        private GameObject _activeLayoutGroup;

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

            DontDestroyOnLoad(Instance.gameObject);
        }

        private void Start()
        {
            Transform contentTransform = transform.Find("Page/Content");

            _drawer = transform.Find("Pools").GetComponent<GUIElementDrawer>();
            _headerText = contentTransform.Find("Header/PageName").GetComponent<TextMeshProUGUI>();
            _headerLogo = contentTransform.Find("Header/PageLogo").GetComponent<RawImage>();
            _headerFitter = _headerLogo.GetComponent<AspectRatioFitter>();

            _background = contentTransform.Find("Viewport/Background").GetComponent<RawImage>();
            _horizontalLayoutGroup = contentTransform.Find("Viewport/HorizontalView").GetComponent<HorizontalLayoutGroup>();
            _verticalLayoutGroup = contentTransform.Find("Viewport/VerticalView").GetComponent<VerticalLayoutGroup>();
            _gridLayoutGroup = contentTransform.Find("Viewport/GridView").GetComponent<GridLayoutGroup>();

            _decrementPageButton = contentTransform.Find("Footer/PreviousPage").GetComponent<Button>();
            _pageIndexText = contentTransform.Find("Footer/PageIndexText").GetComponent<TextMeshProUGUI>();
            _incrementPageButton = contentTransform.Find("Footer/NextPage").GetComponent<Button>();

            _toParentButton = contentTransform.Find("Interaction/Return").GetComponent<Button>();
            _guiDialog = transform.Find("Dialog").GetComponent<GUIDialog>();
            _keyboard = transform.Find("KeyboardMain").GetComponent<Keyboard>();
        }

        private void OnEnable()
        {
            Menu.OnPageOpened += OnPageOpened;
            Menu.OnPageUpdated += OnPageUpdated;
            Dialog.OnDialogCreated += OnDialogCreated;

            _toParentButton.onClick.AddListener(new System.Action(() =>  { ToParentPage(); }));
            _decrementPageButton.onClick.AddListener(new System.Action(() => { Menu.PreviousPage(); }));
            _incrementPageButton.onClick.AddListener(new System.Action(() => { Menu.NextPage(); }));
        }

        private void OnDisable()
        {
            Menu.OnPageOpened -= OnPageOpened;
            Menu.OnPageUpdated -= OnPageUpdated;
            Dialog.OnDialogCreated -= OnDialogCreated;

            _toParentButton.onClick.RemoveListener(new System.Action(() => { ToParentPage(); }));
            _decrementPageButton.onClick.RemoveListener(new System.Action(() => { Menu.PreviousPage(); }));
            _incrementPageButton.onClick.RemoveListener(new System.Action(() => { Menu.NextPage(); }));
        }

        public void OpenKeyboard()
        {
            _keyboard.gameObject.SetActive(true);
        }

        public void ConnectElementToKeyboard(GUIStringElement guiElement)
        {
            _keyboard.ConnectElement(guiElement);
        }

        private void OnPageOpened(Page page)
        {
            if (Menu.CurrentPage != page)
            {
                return;
            }

            SetLayout(page);

            _headerText.gameObject.SetActive(page.Logo == null);
            _headerLogo.gameObject.SetActive(page.Logo != null);

            _headerText.text = page.Name;
            _headerLogo.texture = page.Logo;
            _headerText.color = page.Color;
            _background.texture = page.DefaultBackground;

            if (_headerLogo.texture != null)
            {
                _headerLogo.SetNativeSize();
                
                float width = _headerLogo.texture.width;
                float height = _headerLogo.texture.height;

                _headerFitter.aspectRatio = width / height;
            }
            
            if (page.Parent != null && page.Background == page.DefaultBackground)
            {
                _background.texture = page.Parent.Background;
            }
            else
            {
                _background.texture = page.Background;
            }

            _background.SetNativeSize();

            _drawer.Clear();

            _toParentButton.gameObject.SetActive(page.Parent != null && page != Page.Root);

            if (page.Indexed && page.IsChild)
            {
                Page parent = page.Parent;

                _decrementPageButton.gameObject.SetActive(parent.CurrentSubPage != -1);
                _incrementPageButton.gameObject.SetActive(parent.CurrentSubPage != parent.SubPages.Count - 1);
                _pageIndexText.gameObject.SetActive(true);
                _pageIndexText.text = $"Page: {parent.CurrentSubPage + 1}/{parent.SubPages.Count}";
            }
            else if (page.Indexed && page.SubPages.Count > 0)
            {
                _decrementPageButton.gameObject.SetActive(page.CurrentSubPage != -1);
                _incrementPageButton.gameObject.SetActive(page.CurrentSubPage != page.SubPages.Count - 1);
                _pageIndexText.gameObject.SetActive(true);
                _pageIndexText.text = $"Page: {page.CurrentSubPage + 1}/{page.SubPages.Count}";
            }
            else
            {
                _decrementPageButton.gameObject.SetActive(false);
                _incrementPageButton.gameObject.SetActive(false);
                _pageIndexText.gameObject.SetActive(false);
            }

            _drawer.OnPageUpdated(page);
        }

        private void OnPageUpdated(Page page)
        {
            if (Menu.CurrentPage != page)
            {
                return;
            }
            
            _drawer.Clear();
            _drawer.OnPageUpdated(page);
        }

        private void OnDialogCreated(Dialog dialog)
        {
            _guiDialog.AssignDialog(dialog);
            _guiDialog.Draw();
        }

        private void ToParentPage()
        {
            Menu.OpenParentPage();
        }

        private void SetLayout(Page page)
        {
            float spacing = page.ElementSpacing;
            LayoutType pageLayoutType = page.Layout;

            switch(pageLayoutType)
            {
                case LayoutType.Default:
                case LayoutType.Vertical:
                    _horizontalLayoutGroup.gameObject.SetActive(false);
                    _gridLayoutGroup.gameObject.SetActive(false);
                    _verticalLayoutGroup.gameObject.SetActive(true);
                    ActiveView = _verticalLayoutGroup.transform;
                    break;
                case LayoutType.Horizontal:
                    _gridLayoutGroup.gameObject.SetActive(false);
                    _verticalLayoutGroup.gameObject.SetActive(false);
                    _horizontalLayoutGroup.gameObject.SetActive(true);
                    ActiveView = _horizontalLayoutGroup.transform;
                    break;
                case LayoutType.Grid:
                    _horizontalLayoutGroup.gameObject.SetActive(false);
                    _verticalLayoutGroup.gameObject.SetActive(false);
                    _gridLayoutGroup.gameObject.SetActive(true);
                    ActiveView = _gridLayoutGroup.transform;
                    break;
            }

            _horizontalLayoutGroup.spacing = spacing;
            _verticalLayoutGroup.spacing = spacing;
            _gridLayoutGroup.spacing = new Vector2(spacing, spacing);
        }
    }
}