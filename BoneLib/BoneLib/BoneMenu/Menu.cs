using System;
using System.Collections.Generic;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    public static class Menu
    {
        public static event Action<Page> OnPageCreated;

        public static event Action<Page> OnPageOpened;

        public static event Action<Page> OnPageUpdated;

        public static event Action<Page> OnPageRemoved;

        public static Page CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OpenPage(_currentPage);
            }
        }

        public static Dialog ActiveDialog { get; private set; }

        private static bool _initialized = false;
        private static Page _currentPage;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            Page.Root = new Page("BoneMenu", maxElements: 10);
            OpenPage(Page.Root);

            _initialized = true;
        }

        /// <summary>
        /// Destroys a page. If this page is selected, it will try to go to its parent
        /// when it gets destroyed.
        /// </summary>
        /// <param name="page"></param>
        public static void DestroyPage(Page page)
        {
			if(page == null)
			{
				return;
			}

			if (page.IsIndexedChild && CurrentPage == page)
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

            Internal_OnPageRemoved(page);

            if (page.Parent.TryGetChildPage(page.Name, out _))
            {
                if (CurrentPage == page)
                {
                    OpenPage(page.Parent);
                }

                //page.Parent.ChildPages.Remove(page.Name);
                page.Parent.RemovePageLinks(page);
            }
        }

        public static void OpenPage(Page page)
        {
            if (page == null)
            {
                OpenPage(Page.Root);
            }

            if (page.IndexPages.Count > 0 && page.CurrentIndexPage != -1)
            {
                _currentPage = page.IndexPages[page.CurrentIndexPage];
            }
            else
            {
                _currentPage = page;
            }

            Internal_OnPageOpened(_currentPage);
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

            if (CurrentPage.Parent.CurrentIndexPage == -1)
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
        /// <param name="confirmAction">The code that will run when the "Yes" button is pressed.</param>
        /// <param name="denyAction">The code that will run when the "No" button is pressed.</param>
        public static void DisplayDialog(string title, string message, Texture2D icon = null, Action confirmAction = null, Action denyAction = null)
        {
            Dialog dialog = new Dialog(title, message, icon, confirmAction, denyAction);
            ActiveDialog = dialog;
            dialog.Internal_OnDialogOpened();
        }

        /// <summary>
        /// Displays a dialog that can be used to inform the user.
        /// Useful for when a destructive action is about to be done,
        /// or can serve as an extra information window.
        /// </summary>
        public static void DisplayDialog(DialogData data)
        {
            Dialog dialog = new Dialog(data.Title, data.Message, data.Icon, data.Confirm, data.Deny);
            dialog.SetPrimaryColor(data.Primary);
            dialog.SetSecondaryColor(data.Secondary);
            ActiveDialog = dialog;
            dialog.Internal_OnDialogOpened();
        }

        internal static void Internal_OnPageCreated(Page page)
        {
            OnPageCreated.InvokeActionSafe(page);
        }

        internal static void Internal_OnPageOpened(Page page)
        {
            OnPageOpened.InvokeActionSafe(page);
        }

        internal static void Internal_OnPageUpdated(Page page)
        {
            OnPageUpdated.InvokeActionSafe(page);
        }

        internal static void Internal_OnPageRemoved(Page page)
        {
            OnPageRemoved.InvokeActionSafe(page);
        }
    }
}