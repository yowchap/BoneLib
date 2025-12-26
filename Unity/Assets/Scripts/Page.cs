using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoneLib.BoneMenu
{
    [Serializable]
    public class Page
    {
        public Page()
        {
            _color = Color.white;
        }

        public Page(string name, int maxElements = 0)
        {
            _name = name;
            _color = Color.white;
            _maxElements = maxElements;

            Background = null;
        }

        public Page(Page parent, string name, int maxElements = 0)
        {
            Parent = parent;
            _name = name;
            _color = Color.white;
            _maxElements = maxElements;

            Background = null;
        }

        public Page(string name, Color color, int maxElements = 0)
        {
            _name = name;
            _color = color;
            _maxElements = maxElements;

            Background = null;
        }

        public Page(Page parent, string name, Color color, int maxElements = 0)
        {
            Parent = parent;
            _name = name;
            _color = color;
            _maxElements = maxElements;

            Background = null;
        }

        public static Page Root { get; internal set; }

        public Page this[string name]
        {
            get
            {
                if (TryGetChildPage(name, out Page page))
                {
                    return page;
                }

                return this;
            }
        }

        public Page Parent;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

                Menu.Internal_OnPageUpdated(this);
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                Menu.Internal_OnPageUpdated(this);
            }
        }

        public Texture2D Logo
        {
            get
            {
                return _logo;
            }
            set
            {
                _logo = value;
                Menu.Internal_OnPageUpdated(this);
            }
        }

        public Texture2D Background
        {
            get
            {
                return _background;
            }
            set
            {
                if (value == null)
                {
                    _background = DefaultBackground;
                    Menu.Internal_OnPageUpdated(this);
                    return;
                }

                _background = value;
                Menu.Internal_OnPageUpdated(this);
            }
        }

        public float BackgroundOpacity
        {
            get
            {
                return _backgroundOpacity;
            }
            set
            {
                _backgroundOpacity = Mathf.Clamp01(value);
                Menu.Internal_OnPageUpdated(this);
            }
        }

        public float ElementSpacing
        {
            get
            {
                return _elementSpacing;
            }
            set
            {
                _elementSpacing = value;
                Menu.Internal_OnPageUpdated(this);
            }
        }

        public Texture2D DefaultBackground;

        public IReadOnlyList<Element> Elements => _elements.AsReadOnly();
        public IReadOnlyList<Page> IndexPages => _indexPages.AsReadOnly();
        public int ElementCount => _elements.Count;
        public int CurrentIndexPage => _pageIndex;

        public bool IsIndexedChild { get; private set; }
        public bool Filled => ElementCount == _maxElements;
        public bool Indexed => _maxElements != 0;

        private string _name;
        private Color _color;

        private Texture2D _logo;
        private Texture2D _background;
        private float _backgroundOpacity = 0.85f;
        private float _elementSpacing = 60f;

        private List<Element> _elements = new List<Element>();
        private List<Page> _indexPages = new List<Page>();

        private int _maxElements;

        private int _pageIndex = -1;

        /// <summary>
        /// Adds an element to the page.
        /// If the page is full, and it has max elements set, it will make a new page and add the element there.
        /// </summary>
        /// <param name="element">The element to add.</param>
        public void Add(Element element)
        {
            if (Indexed)
            {
                if (!Filled)
                {
                    _elements.Add(element);
                    Menu.Internal_OnPageUpdated(this);
                    return;
                }

                Page available = FindAvailable();
                if (available != null)
                {
                    AddElementToIndexPage(available, element);
                    return;
                }

                Page indexPage = AddIndexPage();
                AddElementToIndexPage(indexPage, element);
            }
            else
            {
                _elements.Add(element);
                Menu.Internal_OnPageUpdated(this);
            }
        }

        /// <summary>
        /// Removes an element from the page.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public void Remove(Element element)
        {
            if (element == null)
            {
                return;
            }

            _elements.Remove(element);
            Menu.Internal_OnPageUpdated(this);
        }

        /// <summary>
        /// Removes multiple elements from the page.
        /// </summary>
        /// <param name="elements">The group of elements to remove.</param>
        public void Remove(Element[] elements)
        {
            for (int i = elements.Length - 1; i >= 0; i--)
            {
                Element element = elements[i];
                if (_elements.Contains(element))
                {
                    Remove(element);
                }
            }
        }

        /// <summary>
        /// Removes all elements from the page.
        /// </summary>
        public void RemoveAll()
        {
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                //Not using Remove(Element[]) because _elements is a List and don't want to create garbage by doing ToArray
                Remove(_elements[i]);
            }
        }

        /// <summary>
        /// Removes a child page.
        /// </summary>
        /// <param name="page">The page to remove.</param>
        public void RemovePage(Page page)
        {
            Menu.DestroyPage(page);
        }

        /// <summary>
        /// Removes all page links on this page to the target.
        /// </summary>
        /// <param name="page"></param>
        public void RemovePageLinks(Page page)
        {
            for (int i = _elements.Count - 1; i >= 0; i--)
            {
                if (_elements[i] is PageLinkElement link)
                {
                    if (link.LinkedPage == page)
                    {
                        Remove(link);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a list of child pages.
        /// </summary>
        /// <param name="pages">The page to remove.</param>
        public void RemovePages(Page[] pages)
        {
            if (pages.Length == 0)
            {
                return;
            }

            foreach (var page in pages)
            {
                RemovePage(page);
            }
        }

        /// <summary>
        /// Get a child page by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Page GetChildPage(string name)
        {
            foreach (var element in _elements)
            {
                if (element is PageLinkElement link)
                {
                    if (link.LinkedPage.Name == name)
                    {
                        return link.LinkedPage;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get a child page by name, returning if the page existed.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public bool TryGetChildPage(string name, out Page page)
        {
            page = GetChildPage(name);
            return page != null;
        }

        /// <summary>
        /// Looks at the previous indexed page.
        /// </summary>
        /// <returns>The previous indexed page.</returns>
        public Page GetPreviousPage()
        {
            if (_pageIndex == 0)
            {
                return _indexPages[0];
            }
            else if (_indexPages.Count == 0)
            {
                return null;
            }
            else
            {
                return _indexPages[_pageIndex - 1];
            }
        }

        /// <summary>
        /// Looks at the next indexed page.
        /// </summary>
        /// <returns>The next indexed page.</returns>
        public Page GetNextPage()
        {
            if (_pageIndex + 1 >= _indexPages.Count - 1)
            {
                return _indexPages[_indexPages.Count - 1];
            }
            else if (_indexPages.Count == 0)
            {
                return null;
            }
            else
            {
                return _indexPages[_pageIndex + 1];
            }
        }

        /// <summary>
        /// Goes to the next indexed page.
        /// </summary>
        /// <returns></returns>
        public Page NextPage()
        {
            if (_pageIndex >= _indexPages.Count - 1)
            {
                _pageIndex = _indexPages.Count - 1;
            }
            else
            {
                _pageIndex++;
            }

            return _indexPages[_pageIndex];
        }

        /// <summary>
        /// Goes to the previous indexed page.
        /// </summary>
        /// <returns></returns>
        public Page PreviousPage()
        {
            if (_pageIndex == -1)
            {
                _pageIndex = -1;
            }
            else
            {
                _pageIndex--;
            }

            if (_pageIndex <= 0)
            {
                return _indexPages[0];
            }

            return _indexPages[_pageIndex];
        }

        /// <summary>
        /// Creates a page that inherits its name and color from its parent.
        /// </summary>
        /// <returns></returns>
        public Page CreatePage()
        {
            return CreatePage(this.Name, this.Color);
        }

        /// <summary>
        /// Creates a child page with its properties inherited.
        /// If the child page already exists, it will return that existing page.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="maxElements"></param>
        /// <param name="createLink"></param>
        /// <returns></returns>
        public Page CreatePage(string name, Color color, int maxElements = 0, bool createLink = true)
        {
            if (TryGetChildPage(name, out Page page))
            {
                return page;
            }

            page = new Page(parent: this, name, color, maxElements);
            //ChildPages.Add(name, page);
            Menu.Internal_OnPageUpdated(this);

            if (createLink)
            {
                CreatePageLink(page);
            }

            return page;
        }

        public FunctionElement CreateFunction(string name, Color color, Action callback)
        {
            var element = new FunctionElement(name, color, callback);
            Add(element);
            return element;
        }

        public IntElement CreateInt(string name, Color color, int startingValue, int increment, int minValue, int maxValue, Action<int> callback)
        {
            var element = new IntElement(name, color, startingValue, increment, minValue, maxValue, callback);
            Add(element);
            return element;
        }

        public FloatElement CreateFloat(string name, Color color, float startingValue, float increment, float minValue, float maxValue, Action<float> callback)
        {
            var element = new FloatElement(name, color, startingValue, increment, minValue, maxValue, callback);
            Add(element);
            return element;
        }

        public BoolElement CreateBool(string name, Color color, bool startingValue, Action<bool> callback)
        {
            var element = new BoolElement(name, color, startingValue, callback);
            Add(element);
            return element;
        }

        public EnumElement CreateEnum(string name, Color color, Enum value, Action<Enum> callback)
        {
            var element = new EnumElement(name, color, value, callback);
            Add(element);
            return element;
        }

        public StringElement CreateString(string name, Color color, string startingValue, Action<string> callback)
        {
            var element = new StringElement(name, color, startingValue, callback);
            Add(element);
            return element;
        }

        /// <summary>
        /// Creates a function element that, when pressed, takes you to a page.
        /// Can be linked to any page.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PageLinkElement CreatePageLink(Page page)
        {
            var element = new PageLinkElement(page.Name, page.Color, () => { Menu.OpenPage(page); });
            Add(element);

            element.AssignPage(page);

            return element;
        }

        private Page FindAvailable()
        {
            Page found = null;

            for (int i = 0; i < _indexPages.Count; i++)
            {
                if (!_indexPages[i].Filled)
                {
                    found = _indexPages[i];
                    break;
                }
            }

            return found;
        }

        private Page AddIndexPage()
        {
            Page subPage = new Page();
            subPage._name = _name;
            subPage._color = _color;
            subPage._maxElements = _maxElements;
            subPage.Parent = this;
            subPage.IsIndexedChild = true;
            subPage.Background = Background;
            _indexPages.Add(subPage);
            return subPage;
        }

        private void AddElementToIndexPage(Page subPage, Element element)
        {
            if (subPage == null)
            {
                throw new NullReferenceException("Subpage was null when we tried to add an element!");
            }

            if (element == null)
            {
                throw new NullReferenceException("Element doesn't exist when we tried to add it to a subpage!");
            }

            subPage.Add(element);
        }
    }
}