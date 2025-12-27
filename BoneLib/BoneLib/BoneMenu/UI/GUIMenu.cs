using Il2CppCysharp.Threading.Tasks;
using Il2CppInterop.Runtime.Attributes;
using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp(false)]
    public sealed class GUIMenu : MonoBehaviour
    {
        public GUIMenu(System.IntPtr ptr) : base(ptr) { }

        public static GUIMenu Instance { get; private set; }

        public Transform ActiveView { get; private set; }

        private GUIElementDrawer _drawer;
        private TextMeshProUGUI _headerText;
        private RawImage _headerLogo;
        private AspectRatioFitter _headerFitter;
        private ContentSizeFitter _contentSizeFitter;
        private TextMeshProUGUI _pageIndexText;
        private RawImage _background;
        private Button _decrementPageButton;
        private Button _incrementPageButton;
        private Button _toParentButton;
        private ScrollRect _scrollRect;
        private Button _scrollUpButton;
        private Button _scrollDownButton;
        private GUIDialog _guiDialog;
        private VerticalLayoutGroup _verticalLayoutGroup;
        private Keyboard _keyboard;

        private void Awake()
        {
            Instance = this;

            Transform contentTransform = transform.Find("Page/Content");

            _drawer = transform.Find("Pools").GetComponent<GUIElementDrawer>();
            _headerText = contentTransform.Find("Header/PageName").GetComponent<TextMeshProUGUI>();
            _headerLogo = contentTransform.Find("Header/PageLogo").GetComponent<RawImage>();
            _headerFitter = _headerLogo.GetComponent<AspectRatioFitter>();

            _background = contentTransform.Find("Viewport/Background").GetComponent<RawImage>();
            _verticalLayoutGroup = contentTransform.Find("Viewport/VerticalView").GetComponent<VerticalLayoutGroup>();
            _contentSizeFitter = contentTransform.Find("Viewport/VerticalView").GetComponent<ContentSizeFitter>();

            _decrementPageButton = contentTransform.Find("Footer/PreviousPage").GetComponent<Button>();
            _pageIndexText = contentTransform.Find("Footer/PageIndexText").GetComponent<TextMeshProUGUI>();
            _incrementPageButton = contentTransform.Find("Footer/NextPage").GetComponent<Button>();

            _scrollRect = contentTransform.Find("Viewport").GetComponent<ScrollRect>();
            _scrollUpButton = contentTransform.Find("Interaction/ScrollUp").GetComponent<Button>();
            _scrollDownButton = contentTransform.Find("Interaction/ScrollDown").GetComponent<Button>();

            _toParentButton = contentTransform.Find("Interaction/Return").GetComponent<Button>();
            _guiDialog = transform.Find("Dialog").GetComponent<GUIDialog>();
            _guiDialog.gameObject.SetActive(true);

            _keyboard = transform.Find("Keyboard").GetComponent<Keyboard>();

            _keyboard.gameObject.SetActive(false);

            ActiveView = _verticalLayoutGroup.transform;
        }

        private void OnEnable()
        {
            Menu.OnPageOpened += OnPageOpened;
            Menu.OnPageUpdated += OnPageUpdated;
            Dialog.OnDialogOpened += OnDialogOpened;
            Dialog.OnDialogClosed += OnDialogClosed;

            _scrollUpButton.onClick.AddListener(new System.Action(ScrollUp));
            _scrollDownButton.onClick.AddListener(new System.Action(ScrollDown));

            _toParentButton.onClick.AddListener(new System.Action(ToParentPage));
            _decrementPageButton.onClick.AddListener(new System.Action(Menu.PreviousPage));
            _incrementPageButton.onClick.AddListener(new System.Action(Menu.NextPage));
        }

        private void OnDisable()
        {
            Menu.OnPageOpened -= OnPageOpened;
            Menu.OnPageUpdated -= OnPageUpdated;
            Dialog.OnDialogOpened -= OnDialogOpened;
            Dialog.OnDialogClosed -= OnDialogClosed;

            _toParentButton.onClick.RemoveAllListeners();
            _scrollUpButton.onClick.RemoveAllListeners();
            _scrollDownButton.onClick.RemoveAllListeners();
            _decrementPageButton.onClick.RemoveAllListeners();
            _incrementPageButton.onClick.RemoveAllListeners();

            // Restore the background, just in case if we click off
            MenuBootstrap.background.SetActive(true);
            // Disable any active dialogs
            _guiDialog.gameObject.SetActive(false);
        }

        public void OpenKeyboard()
        {
            _keyboard.gameObject.SetActive(true);

            // Might be hacky, but this just disables
            // elements behind the keyboard so we don't click them by accident
            ShowView(false);
            _toParentButton.gameObject.SetActive(false);
        }

        public void CloseKeyboard()
        {
            _keyboard.gameObject.SetActive(false);
            // Turns the layout object back on again
            ShowView(true);
            _toParentButton.gameObject.SetActive(true);
        }

        public void ShowView(bool show = true)
        {
            ActiveView.gameObject.SetActive(show);
        }

        public void ScrollUp()
        {
            float elementSpacing = _verticalLayoutGroup.spacing;
            int numberOfElements = Menu.CurrentPage.ElementCount;
            _scrollRect.velocity = Vector2.down * (elementSpacing * numberOfElements) / 2f;
        }

        public void ScrollDown()
        {
            float elementSpacing = _verticalLayoutGroup.spacing;
            int numberOfElements = Menu.CurrentPage.ElementCount;
            _scrollRect.velocity = Vector2.up * (elementSpacing * numberOfElements) / 2f;
        }

        public void ConnectElementToKeyboard(GUIStringElement guiElement)
        {
            _keyboard.ConnectElement(guiElement);
        }

        [HideFromIl2Cpp]
        private void OnPageOpened(Page page)
        {
            if (Menu.CurrentPage != page)
            {
                return;
            }

            SetLayout(page);
            DrawHeader(page);
            DrawBackground(page);
            DrawFooter(page);
            DrawElements(page);
        }

        private void DrawHeader(Page page)
        {
            _headerText.gameObject.SetActive(page.Logo == null);
            _headerLogo.gameObject.SetActive(page.Logo != null);

            _headerText.text = page.Name;
            _headerLogo.texture = page.Logo;
            _headerText.color = page.Color;

            if (_headerLogo.texture != null)
            {
                _headerLogo.SetNativeSize();

                float width = _headerLogo.texture.width;
                float height = _headerLogo.texture.height;

                _headerFitter.aspectRatio = width / height;
            }
        }

        private void DrawBackground(Page page)
        {
            _background.texture = page.Background;
            _background.SetNativeSize();
            _background.color = new Color(_background.color.r, _background.color.g, _background.color.b, page.BackgroundOpacity);
        }

        private void DrawFooter(Page page)
        {
            if (page.Indexed && page.IsIndexedChild)
            {
                Page parent = page.Parent;

                _decrementPageButton.gameObject.SetActive(parent.CurrentIndexPage != -1);
                _incrementPageButton.gameObject.SetActive(parent.CurrentIndexPage != parent.IndexPages.Count - 1);
                _pageIndexText.gameObject.SetActive(true);
                _pageIndexText.text = $"Page: {parent.CurrentIndexPage + 1}/{parent.IndexPages.Count}";
            }
            else if (page.Indexed && page.IndexPages.Count > 0)
            {
                _decrementPageButton.gameObject.SetActive(page.CurrentIndexPage != -1);
                _incrementPageButton.gameObject.SetActive(page.CurrentIndexPage != page.IndexPages.Count - 1);
                _pageIndexText.gameObject.SetActive(true);
                _pageIndexText.text = $"Page: {page.CurrentIndexPage + 1}/{page.IndexPages.Count}";
            }
            else
            {
                _decrementPageButton.gameObject.SetActive(false);
                _incrementPageButton.gameObject.SetActive(false);
                _pageIndexText.gameObject.SetActive(false);
            }
        }

        private void DrawElements(Page page)
        {
            _drawer.Clear();
            _drawer.OnPageUpdated(page);
        }

        [HideFromIl2Cpp]
        private void OnPageUpdated(Page page)
        {
            if (Menu.CurrentPage != page)
            {
                return;
            }
            
            _drawer.Clear();
            _drawer.OnPageUpdated(page);
        }

        [HideFromIl2Cpp]
        private void OnDialogOpened(Dialog dialog)
        {
            _guiDialog.AssignDialog(dialog);
            _guiDialog.Draw();

            // anything behind the dialog will be selectable
            // just disable both the view and the keyboard
            _keyboard.gameObject.SetActive(false);
            ActiveView.gameObject.SetActive(false);
        }

        [HideFromIl2Cpp]
        private void OnDialogClosed(Dialog dialog)
        {
            _guiDialog.gameObject.SetActive(false);
            ActiveView.gameObject.SetActive(true);
        }

        private void ToParentPage()
        {
            if (Menu.CurrentPage.Name == Page.Root.Name)
            {
                // Go back to default game page
                MenuBootstrap.ResetGameMenu();
                MenuBootstrap.panelView.PAGESELECT(MenuBootstrap.panelView.defaultPage);
                CloseKeyboard();
                return;
            }

            Menu.OpenParentPage();
        }

        [HideFromIl2Cpp]
        private void SetLayout(Page page)
        {
            int elements = page.ElementCount;
            float spacing = page.ElementSpacing;
            _verticalLayoutGroup.spacing = spacing;
            // ActiveView.transform.position = Vector3.down * elements * spacing;
        }
    }
}