using SLZ.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnityEngine;

namespace BoneLib.RadialMenu
{
    public class RadialButton
    {
        public PageItem PageItem { get; internal set; }
        public Sprite Icon { get; set; }

        internal RadialButton(string text, Action action, PageItem.Directions direction, Sprite icon = null)
        {
            PageItem pageItem = new(text, direction, action);
            PageItem = pageItem;

            Icon = icon;
        }

        internal RadialButton(string text, Il2CppSystem.Action action, PageItem.Directions direction, Sprite icon = null) 
        {
            PageItem pageItem = new(text, direction, action);
            PageItem = pageItem;

            Icon = icon;
        }
    }
}
