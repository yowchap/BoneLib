using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BoneLib.UI
{
    public class Page : MonoBehaviour
    {
        public string Title { get; set; }

        public List<Element> elements;

        public void Start()
        {
            elements = gameObject.GetComponentsInChildren<Element>().ToList();
        }
    }
}