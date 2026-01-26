using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    public class GUIElementDrawer: MonoBehaviour
    {
        private GUIMenu _menu;
        private GUIPool _functionPool;
        private GUIPool _intPool;
        private GUIPool _floatPool;
        private GUIPool _boolPool;
        private GUIPool _enumPool;
        private GUIPool _stringPool;

        private void Awake()
        {
            _menu = transform.parent.GetComponent<GUIMenu>();
            _functionPool = transform.Find("Function").GetComponent<GUIPool>();
            _intPool = transform.Find("Integer").GetComponent<GUIPool>();
            _floatPool = transform.Find("Float").GetComponent<GUIPool>();
            _boolPool = transform.Find("Boolean").GetComponent<GUIPool>();
            _enumPool = transform.Find("Enum").GetComponent<GUIPool>();
            _stringPool = transform.Find("String").GetComponent<GUIPool>();

            // _functionPool.SetPrefab(MenuBootstrap.functionPrefab);
            // _intPool.SetPrefab(MenuBootstrap.intPrefab);
            // _floatPool.SetPrefab(MenuBootstrap.floatPrefab);
            // _boolPool.SetPrefab(MenuBootstrap.boolPrefab);
            // _enumPool.SetPrefab(MenuBootstrap.enumPrefab);
            // _stringPool.SetPrefab(MenuBootstrap.stringPrefab);

            _functionPool.Initialize();
            _intPool.Initialize();
            _floatPool.Initialize();
            _boolPool.Initialize();
            _enumPool.Initialize();
            _stringPool.Initialize();
        }

        public void OnPageUpdated(Page page)
        {
            for (int i = 0; i < page.ElementCount; i++)
            {
                OnElementAdded(page.Elements[i]);
            }
        }

        public void Clear()
        {
            _functionPool.ReturnAll();
            _intPool.ReturnAll();
            _floatPool.ReturnAll();
            _boolPool.ReturnAll();
            _enumPool.ReturnAll();
            _stringPool.ReturnAll();
        }

        public void OnElementAdded(Element element)
        {
            if (element is FunctionElement functionElement)
            {
                var guiFunctionElement = _functionPool.Spawn(_menu.ActiveView).GetComponent<GUIFunctionElement>();
                guiFunctionElement.AssignElement(functionElement);
                guiFunctionElement.Draw();
            }

            if (element is IntElement intElement)
            {
                var guiIntElement = _intPool.Spawn(_menu.ActiveView).GetComponent<GUIIntElement>();
                guiIntElement.AssignElement(intElement);
                guiIntElement.Draw();
            }

            if (element is FloatElement floatElement)
            {
                var guiFloatElement = _floatPool.Spawn(_menu.ActiveView).GetComponent<GUIFloatElement>();
                guiFloatElement.AssignElement(floatElement);
                guiFloatElement.Draw();
            }

            if (element is BoolElement boolElement)
            {
                var guiBoolElement = _boolPool.Spawn(_menu.ActiveView).GetComponent<GUIBoolElement>();
                guiBoolElement.AssignElement(boolElement);
                guiBoolElement.Draw();
            }

            if (element is StringElement stringElement)
            {
                var guiStringElement = _stringPool.Spawn(_menu.ActiveView).GetComponent<GUIStringElement>();
                guiStringElement.AssignElement(stringElement);
                guiStringElement.Draw();
            }

            if (element is EnumElement enumElement)
            {
                var guiEnumElement = _enumPool.Spawn(_menu.ActiveView).GetComponent<GUIEnumElement>();
                guiEnumElement.AssignElement(enumElement);
                guiEnumElement.Draw();
            }
        }
    }
}