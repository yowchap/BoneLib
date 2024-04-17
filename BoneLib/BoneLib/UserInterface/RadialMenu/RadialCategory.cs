using MelonLoader;
using SLZ.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BoneLib.RadialMenu;

namespace BoneLib.RadialMenu 
{
    public class RadialCategory
    {
        public string Name { get; internal set; }
        public Color Color { get; set; }


        internal List<RadialButton> Buttons = new();

        /// <summary>
        /// Creates a RadialCategory with the given name and adds it to the Menu list.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        public RadialCategory(string name, Color color)
        {
            Name = name;
            Color = color;

            var leftCycleItem = new RadialButton("<----", () => RadialMenuManager.CycleLeft(), PageItem.Directions.SOUTHWEST);
            var rightCycleItem = new RadialButton("---->", () => RadialMenuManager.CycleRight(), PageItem.Directions.SOUTHEAST);
            TryAddButton(leftCycleItem);
            TryAddButton(rightCycleItem);

            RadialMenuManager.AddRadialCategory(this);
        }

        internal RadialCategory() { }

        /// <summary>
        /// Returns false if the slot is full.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool TryAddButton(RadialButton button)
        {
            if (Buttons.Count > 7)
                return false;

            if (Buttons.ToArray().Any(x => x.PageItem.direction == button.PageItem.direction))
                return false;

            Buttons.Add(button);

            if (Player.uiRig != null)
                RadialMenuManager.RefreshRadialCategory(RadialMenuManager.ActiveCategory);

            return true;
        }

        /// <summary>
        /// Returns false if the button is not found in the category.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool TryRemoveButton(RadialButton button)
        {
            if (Buttons.Contains(button))
            {
                Buttons.Remove(button);

                if (Player.uiRig != null)
                    RadialMenuManager.RefreshRadialCategory(RadialMenuManager.ActiveCategory);

                return true;
            }

            return false;
        }
    }
}
