using SLZ.UI.Radial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BoneLib.UI.Panels
{
    public class ModListPanelView : PanelView
    {
        private List<GameObject> pages = new List<GameObject>();
        private int currentPage = 0;

        public void Populate()
        {
            // Add all mods.
        }

        public void NextPage()
        {
            if (currentPage < pages.Count) return;

            currentPage++;
            Refresh();
        }

        public void PrevPage()
        {
            if (currentPage > 0) return;

            currentPage--;
            Refresh();
        }

        public void SetPage(int i)
        {
            if (i < pages.Count && i > 0)
                currentPage = i;
        }

        public void Refresh()
        {
            foreach (GameObject page in pages)
            {
                page.SetActive(pages.IndexOf(page) == currentPage);
            }
        }
    }
}