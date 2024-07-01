using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public static class Menu
    {
        public static Action<Page> OnPageCreated;
        public static Action<Page> OnPageOpened;
        public static Action<Page> OnPageUpdated;
        public static Action<Page> OnPageRemoved;

        public static Page CurrentPage;
        public static Dialog ActiveDialog { get; private set; }
        public static Dictionary<string, Page> PageDirectory;

        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            PageDirectory = new Dictionary<string, Page>();

            Page.Root = CreatePage("BoneMenu", maxElements: 10);
            Page.Root.SetLayout(LayoutType.Vertical);
            // OpenPage(Page.Root);

            _initialized = true;
        }

        public static Page CreatePage(string name, int maxElements = 0)
        {
            return CreatePage(Page.Root, name, Color.white, maxElements);
        }

        public static Page CreatePage(Page parent, string name, int maxElements = 0)
        {
            return CreatePage(parent, name, Color.white, maxElements);
        }

        public static Page CreatePage(string name, Color color, int maxElements = 0)
        {
            return CreatePage(Page.Root, name, color, maxElements);
        }

        public static Page CreatePage(Page parent, string name, Color color, int maxElements = 0)
        {
            if (PageDirectory.ContainsKey(name))
            {
                return PageDirectory[name];
            }

            Page page = new Page(parent, name, color, maxElements);
            OnPageCreated?.Invoke(page);
            PageDirectory?.Add(page.Name, page);
            return page;
        }

        public static void DestroyPage(Page page)
        {
            if (page.IsChild)
            {
                Debug.Log("Page is child");
                
                if (page.Parent.GetNextPage() != null)
                {
                    Debug.Log("Available next page, going there");
                    OpenPage(page.Parent.NextPage());
                    return;
                }

                if (page.Parent.GetPreviousPage() != null)
                {
                    Debug.Log("Available previous page, going there");
                    OpenPage(page.Parent.PreviousPage());
                    return;
                }
            }
        }

        public static void OpenPage(string pageName)
        {
            if (!PageDirectory.ContainsKey(pageName))
            {
                OpenPage(Page.Root);
                throw new KeyNotFoundException("Page does not exist!");
            }

            OpenPage(PageDirectory[pageName]);
        }

        public static void OpenPage(Page page)
        {
            if (page == null)
            {
                OpenPage(Page.Root);
            }

            if (page.SubPages.Count > 0 && page.CurrentSubPage != -1)
            {
                CurrentPage = page.SubPages[page.CurrentSubPage];
            }
            else
            {
                CurrentPage = page;
            }
            
            OnPageOpened?.Invoke(CurrentPage);
        }

        public static void PreviousPage()
        {
            if (!CurrentPage.IsChild)
            {
                return;
            }

            Page previousPage = CurrentPage.Parent.PreviousPage();

            if (CurrentPage.Parent.CurrentSubPage == -1)
            {
                OpenPage(CurrentPage.Parent);
                return;
            }

            OpenPage(previousPage);
        }

        public static void NextPage()
        {
            if (CurrentPage.IsChild)
            {
                OpenPage(CurrentPage.Parent.NextPage());
            }
            else
            {
                OpenPage(CurrentPage.NextPage());
            }
        }

        public static void OpenParentPage()
        {
            if (CurrentPage.IsChild)
            {
                OpenPage(CurrentPage.Parent.Parent);
                return;
            }
            else
            {
                OpenPage(CurrentPage.Parent);
            }
        }

        public static void DisplayDialog(string title, string message, Texture2D icon, Dialog.Options options = Dialog.Options.YesOption | Dialog.Options.NoOption)
        {
            Dialog dialog = new Dialog(title, message, icon, options);
            ActiveDialog = dialog;
            Dialog.OnDialogCreated?.Invoke(ActiveDialog);
        }
    }
}