using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoneLib.UI
{
    public class Page
    {
        public string Title { get; set; }
        public List<Element> Elements { get; set; }

        public Page(string title)
        {
            Title = title;
        }

        public Element[] GetElementsOfType<T>(T elementType) where T : Element
        {
            return Elements.Where(e => e.GetType() == elementType.GetType()).ToArray();
        }

        public void AddElement(Element e) => Elements.Add(e);

        public void RemoveElement(Element e) => Elements.Remove(e);
    }
}