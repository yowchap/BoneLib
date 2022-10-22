using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoneLib.UI.Panels;
using SLZ.UI.Radial;
using UnityEngine;

namespace BoneLib.UI
{
    public static class PanelFactory
    {
        public static ModListPanelView CreateModListPanel()
        {
            var panelOBJ = GameObject.Instantiate(UIResources.listPanelPrefab);
            panelOBJ.transform.parent = PopUpMenuView.instance.levelsPanelView.transform.parent;

            var panel = panelOBJ.GetComponent<ModListPanelView>();
            panel.popUpMenu = PopUpMenuView.instance;
            panel.Populate();

            return panel;
        }
    }
}