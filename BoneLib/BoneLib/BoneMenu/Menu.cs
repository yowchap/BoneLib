using BoneLib.BoneMenu.UI;
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

            Page.Root = new Page("BoneMenu", maxElements: 10);
            Page.Root.SetLayout(LayoutType.Vertical);
            OpenPage(Page.Root);

            _initialized = true;
        }

        public static Page CreatePage(string name, int maxElements = 0, bool createLink = true)
        {
            return CreatePage(Page.Root, name, Color.white, maxElements, createLink);
        }

        public static Page CreatePage(Page parent, string name, int maxElements = 0, bool createLink = true)
        {
            return CreatePage(parent, name, Color.white, maxElements, createLink);
        }

        public static Page CreatePage(string name, Color color, int maxElements = 0, bool createLink = true)
        {
            return CreatePage(Page.Root, name, color, maxElements, createLink);
        }

        public static Page CreatePage(Page parent, string name, Color color, int maxElements = 0, bool createLink = true)
        {
            if (PageDirectory.ContainsKey(name))
            {
                return PageDirectory[name];
            }

            Page page = new Page(parent, name, color, maxElements);
            OnPageCreated?.Invoke(page);
            PageDirectory?.Add(page.Name, page);

            if (createLink)
            {
                parent.CreatePageLink(page);
            }

            return page;
        }

        /// <summary>
        /// "Destroys" a page. If this page is selected, it will try to go to its parent
        /// when it gets destroyed.
        /// </summary>
        /// <param name="page"></param>
        public static void DestroyPage(Page page)
        {
            if (page.IsIndexedChild)
            {
                if (page.Parent.GetNextPage() != null)
                {
                    OpenPage(page.Parent.NextPage());
                    return;
                }

                if (page.Parent.GetPreviousPage() != null)
                {
                    OpenPage(page.Parent.PreviousPage());
                    return;
                }
            }

            OnPageRemoved?.Invoke(page);
            PageDirectory?.Remove(page.Name);

            if (page.Parent.ChildPages.ContainsKey(page.Name))
            {
                page.Parent.ChildPages.Remove(page.Name);
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

        /// <summary>
        /// Navigates to the previous child page.
        /// </summary>
        public static void PreviousPage()
        {
            if (!CurrentPage.IsIndexedChild)
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

        /// <summary>
        /// Navigates to the next child page.
        /// </summary>
        public static void NextPage()
        {
            if (CurrentPage.IsIndexedChild)
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
            if (CurrentPage.IsIndexedChild)
            {
                // Go to our parents parent
                OpenPage(CurrentPage.Parent.Parent);
            }
            else
            {
                OpenPage(CurrentPage.Parent);
            }
        }

        /// <summary>
        /// Displays a dialog that can be used to inform the user. 
        /// Useful for when a destructive action is about to be done, 
        /// or can serve as an extra information window.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="message">The message that will be displayed to the user.</param>
        /// <param name="icon">The icon that will sit alongside the title. Optional.</param>
        /// <param name="options">The buttons that will be displayed for input.</param>
        public static void DisplayDialog(string title, string message, Texture2D icon = null, Dialog.Options options = Dialog.Options.YesOption | Dialog.Options.NoOption)
        {
            Dialog dialog = new Dialog(title, message, icon, options);
            ActiveDialog = dialog;
            Dialog.OnDialogCreated?.Invoke(ActiveDialog);
        }
    }
}