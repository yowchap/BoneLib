using System.Collections.Generic;
using System.Linq;

namespace BoneLib.UI
{
    public class Page
    {
        public string Title { get; set; }

        public List<Element> elements;

        public Page(string title)
        {
            Title = title;
        }

        public T[] GetElementsOfType<T>() where T : Element
        {
            return elements.OfType<T>().ToArray();
        }

        public void AddElement(Element e)
        {
            elements.Add(e);
            e.parentPage = this;
        }

        public void RemoveElement(Element e)
        {
            elements.Remove(e);
            e.parentPage = null;
        }
    }
}