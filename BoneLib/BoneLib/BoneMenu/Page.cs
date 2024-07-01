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
            
            SetBackground(DefaultBackground);
        }

        public Page(Page parent, string name, int maxElements = 0)
        {
            Parent = parent;
            _name = name;
            _color = Color.white;
            _maxElements = maxElements;

            SetBackground(DefaultBackground);
        }

        public Page(string name, Color color, int maxElements = 0)
        {
            _name = name;
            _color = color;
            _maxElements = maxElements;

            SetBackground(DefaultBackground);
        }

        public Page(Page parent, string name, Color color, int maxElements = 0)
        {
            Parent = parent;
            _name = name;
            _color = color;
            _maxElements = maxElements;

            SetBackground(DefaultBackground);
        }

        public static Page Root;

        public Page Parent;

        public string Name => _name;
        public Color Color => _color;
        public Texture2D Logo { get; set; }
        public Texture2D Background { get; set; }
        public Texture2D DefaultBackground { get; private set; } = Resources.Load<Texture2D>("sprite_blackGrid_blur");
        public LayoutType Layout { get; set; }
        public float ElementSpacing { get; set; } = 60;

        public IReadOnlyList<Element> Elements => _elements.AsReadOnly();
        public IReadOnlyList<SubPage> SubPages => _subPages.AsReadOnly();
        public int ElementCount => _numElements;
        public int CurrentSubPage => _subPageIndex;

        public bool IsChild { get; private set; }
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

        public void Remove(Element element)
        {
            _elements.Remove(element);
            _numElements--;
            Menu.OnPageUpdated?.Invoke(this);
        }

        public void Remove(Element[] elements)
        {
            HashSet<Element> query = _elements.ToHashSet();
            query.IntersectWith(elements);

            foreach (Element queryElement in query)
            {
                _elements.Remove(queryElement);
            }

            _numElements = _elements.Count;

            Debug.Log(_numElements);

            if (_numElements == 0)
            {
                Parent._subPages.Remove(this as SubPage);
                Menu.DestroyPage(this);
            }

            Menu.OnPageUpdated?.Invoke(this);
        }

        public void RemoveAll() => Remove(_elements.ToArray());

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

        public void SetLogo(Texture2D logo)
        {
            Logo = logo;
            Menu.OnPageUpdated?.Invoke(this);
        }

        public void SetBackground(Texture2D background)
        {
            if (background == null)
            {
                // TODO: Use default grid background
            }

            Background = background;
            Menu.OnPageUpdated?.Invoke(this);
        }

        public void SetLayout(LayoutType layoutType)
        {
            Layout = layoutType;
            Menu.OnPageUpdated?.Invoke(this);
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
            subPage.IsChild = true;
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