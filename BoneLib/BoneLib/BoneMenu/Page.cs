using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace BoneLib.BoneMenu
{
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
                if (ChildPages.TryGetValue(name, out Page page))
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

        public Texture2D DefaultBackground => MenuBootstrap.defaultBackgroundTexture;

        public IReadOnlyList<Element> Elements => _elements.AsReadOnly();
        public IReadOnlyList<SubPage> SubPages => _subPages.AsReadOnly();
        public Dictionary<string, Page> ChildPages = new Dictionary<string, Page>();
        public int ElementCount => _elements.Count;
        public int CurrentSubPage => _subPageIndex;

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
        private List<SubPage> _subPages = new List<SubPage>();
        private List<PageLinkElement> _links = new List<PageLinkElement>();

        private int _maxElements;

        private int _subPageIndex = -1;

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

                SubPage available = FindAvailable();
                if (available != null)
                {
                    AddElementToSubPage(available, element);
                    return;
                }

                SubPage subPage = AddSubPage();
                AddElementToSubPage(subPage, element);
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
            if (element is PageLinkElement link)
            {
                _links.Remove(link);
            }

            _elements.Remove(element);
            Menu.Internal_OnPageUpdated(this);
        }

        /// <summary>
        /// Removes multiple elements from the page.
        /// If the page contains no elements, the page gets destroyed.
        /// </summary>
        /// <param name="elements">The group of elements to remove.</param>
        public void Remove(Element[] elements)
        {
            HashSet<Element> query = _elements.ToHashSet();
            query.IntersectWith(elements);

            foreach (Element queryElement in query)
            {
                if (queryElement is PageLinkElement link)
                {
                    _links.Remove(link);
                }

                _elements.Remove(queryElement);
            }

            if (ElementCount == 0)
            {
                Parent._subPages.Remove(this as SubPage);
                Menu.DestroyPage(this);
            }

            Menu.Internal_OnPageUpdated(this);
        }

        /// <summary>
        /// Removes all elements from the page.
        /// </summary>
        public void RemoveAll() => Remove(_elements.ToArray());

        /// <summary>
        /// Removes a child page.
        /// </summary>
        /// <param name="page">The page to remove.</param>
        public void RemovePage(Page page)
        {
            if (page == null || string.IsNullOrEmpty(page.Name) || ChildPages.Count == 0)
            {
                return;
            }

            if (!ChildPages.TryGetValue(page.Name, out Page child))
            {
                ModConsole.Error($"Failed to remove child page {page.Name}");
                return;
            }

            for (int i = 0; i < _links.Count; i++)
            {
                if (_links[i].LinkedPage == child)
                {
                    Remove(_links[i]);
                }
            }

            Menu.DestroyPage(child);
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
        /// Looks at the previous indexed page.
        /// </summary>
        /// <returns>The previous indexed page.</returns>
        public Page GetPreviousPage()
        {
            if (_subPageIndex == 0)
            {
                return _subPages[0];
            }
            else if (_subPages.Count == 0)
            {
                return null;
            }
            else
            {
                return _subPages[_subPageIndex - 1];
            }
        }

        /// <summary>
        /// Looks at the next indexed page.
        /// </summary>
        /// <returns>The next indexed page.</returns>
        public Page GetNextPage()
        {
            if (_subPageIndex + 1 >= _subPages.Count - 1)
            {
                return _subPages[_subPages.Count - 1];
            }
            else if (_subPages.Count == 0)
            {
                return null;
            }
            else
            {
                return _subPages[_subPageIndex + 1];
            }
        }

        /// <summary>
        /// Goes to the next indexed page.
        /// </summary>
        /// <returns></returns>
        public Page NextPage()
        {
            if (_subPageIndex >= _subPages.Count - 1)
            {
                _subPageIndex = _subPages.Count - 1;
            }
            else
            {
                _subPageIndex++;
            }

            return _subPages[_subPageIndex];
        }
        
        /// <summary>
        /// Goes to the previous indexed page.
        /// </summary>
        /// <returns></returns>
        public Page PreviousPage()
        {
            if (_subPageIndex == -1)
            {
                _subPageIndex = -1;
            }
            else
            {
                _subPageIndex--;
            }

            if (_subPageIndex <= 0)
            {
                return _subPages[0];
            }

            return _subPages[_subPageIndex];
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
            if (ChildPages.TryGetValue(name, out Page page))
            {
                return page;
            }

            page = new Page(parent: this, name, color, maxElements);
            ChildPages.Add(name, page);
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
            _links.Add(element);

            return element;
        }

        private SubPage FindAvailable()
        {
            SubPage found = null;

            for (int i = 0; i < _subPages.Count; i++)
            {
                if (!_subPages[i].Filled)
                {
                    found = _subPages[i];
                    break;
                }
            }

            return found;
        }

        private SubPage AddSubPage()
        {
            SubPage subPage = new SubPage();
            subPage._name = _name;
            subPage._color = _color;
            subPage._maxElements = _maxElements;
            subPage.Parent = this;
            subPage.IsIndexedChild = true;
            subPage.Background = Background;
            _subPages.Add(subPage);
            return subPage;
        }

        private void AddElementToSubPage(SubPage subPage, Element element)
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