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

        }

        public Page(string name, int maxElements = 0)
        {
            _name = name;
            _color = Color.white;
            _maxElements = maxElements;
            
            SetBackground(null);
        }

        public Page(Page parent, string name, int maxElements = 0)
        {
            Parent = parent;
            _name = name;
            _color = Color.white;
            _maxElements = maxElements;

            SetBackground(null);
        }

        public Page(string name, Color color, int maxElements = 0)
        {
            _name = name;
            _color = color;
            _maxElements = maxElements;

            SetBackground(null);
        }

        public Page(Page parent, string name, Color color, int maxElements = 0)
        {
            Parent = parent;
            _name = name;
            _color = color;
            _maxElements = maxElements;

            SetBackground(null);
        }

        public static Page Root;

        public Page this[string name]
        {
            get
            {
                if (ChildPages.ContainsKey(name))
                {
                    return ChildPages[name];
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
                Menu.OnPageOpened?.Invoke(this);
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
                Menu.OnPageOpened?.Invoke(this);
            }
        }

        public Texture2D Logo { get; set; }
        public Texture2D Background { get; set; }
        public Texture2D DefaultBackground => MenuBootstrap.defaultBackgroundTexture;
        public float BackgroundOpacity { get; set; } = 0.85f;

        public LayoutType Layout { get; set; }
        public float ElementSpacing { get; set; } = 60;

        public IReadOnlyList<Element> Elements => _elements.AsReadOnly();
        public IReadOnlyList<SubPage> SubPages => _subPages.AsReadOnly();
        public Dictionary<string, Page> ChildPages = new Dictionary<string, Page>();
        public int ElementCount => _numElements;
        public int CurrentSubPage => _subPageIndex;

        public bool IsIndexedChild { get; private set; }
        public bool Filled => _numElements == _maxElements;
        public bool Indexed => _maxElements != 0;

        private string dbg_ParentName;
        private string _name;
        private Color _color;
        private List<Element> _elements = new List<Element>();
        private List<SubPage> _subPages = new List<SubPage>();

        private int _numElements;
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
                    _numElements++;
                    Menu.OnPageUpdated?.Invoke(this);
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
                _numElements++;
                Menu.OnPageUpdated?.Invoke(this);
            }
        }

        /// <summary>
        /// Removes an element from the page.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public void Remove(Element element)
        {
            _elements.Remove(element);
            _numElements--;
            Menu.OnPageUpdated?.Invoke(this);
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
                _elements.Remove(queryElement);
            }

            _numElements = _elements.Count;

            if (_numElements == 0)
            {
                Parent._subPages.Remove(this as SubPage);
                Menu.DestroyPage(this);
            }

            Menu.OnPageUpdated?.Invoke(this);
        }

        /// <summary>
        /// Removes all elements from the page.
        /// </summary>
        public void RemoveAll() => Remove(_elements.ToArray());

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
        /// Sets the logo that will be displayed on the page's header.
        /// </summary>
        /// <param name="logo"></param>
        public void SetLogo(Texture2D logo)
        {
            Logo = logo;
            Menu.OnPageUpdated?.Invoke(this);
        }

        /// <summary>
        /// Sets the background. Ideally you should use a square texture,
        /// but you can use any aspect ratio image of your choice.
        /// </summary>
        /// <param name="background"></param>
        public void SetBackground(Texture2D background)
        {
            if (background == null)
            {
                Background = DefaultBackground;
                Menu.OnPageUpdated?.Invoke(this);
                return;
            }

            Background = background;
            Menu.OnPageUpdated?.Invoke(this);
        }

        /// <summary>
        /// Sets the background opacity, or how much the background is transparent/see-through.
        /// </summary>
        /// <param name="opacity"></param>
        public void SetBackgroundOpacity(float opacity)
        {
            if (opacity > 1)
            {
                opacity = 1;
            }

            if (opacity < 0)
            {
                opacity = 0;
            }

            BackgroundOpacity = opacity;
            Menu.OnPageUpdated?.Invoke(this);
        }

        /// <summary>
        /// Sets the layout of the page.
        /// This will affect the layout group of the page, and how elements are ordered.
        /// </summary>
        /// <param name="layoutType"></param>
        public void SetLayout(LayoutType layoutType)
        {
            Layout = layoutType;
            Menu.OnPageUpdated?.Invoke(this);
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
        /// <returns></returns>
        public Page CreatePage(string name, Color color, int maxElements = 0)
        {
            if (ChildPages.ContainsKey(name))
            {
                return ChildPages[name];
            }

            Page page = new Page(parent: this, name, color, maxElements);
            ChildPages.Add(name, page);
            Menu.OnPageCreated?.Invoke(this);
            return page;
        }

        public FunctionElement CreateFunction(string name, Color color, Action callback)
        {
            var element = new FunctionElement(name, color, callback);
            Add(element);
            return element;
        }

        public IntElement CreateInt(string name, Color color, int increment, int startingValue, int minValue, int maxValue, Action<int> callback)
        {
            var element = new IntElement(name, color, increment, startingValue, minValue, maxValue, callback);
            Add(element);
            return element;
        }

        public FloatElement CreateFloat(string name, Color color, float increment, float startingValue, float minValue, float maxValue, Action<float> callback)
        {
            var element = new FloatElement(name, color, increment, startingValue, minValue, maxValue, callback);
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
            var element = new EnumElement(name, color, value);
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
        public FunctionElement CreatePageLink(Page page)
        {
            return CreateFunction(page.Name, page.Color, () => { Menu.OpenPage(page); });
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