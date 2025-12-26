using TMPro;
using UnityEngine;

namespace BoneLib.BoneMenu.UI
{
    public class GUIInfoBox : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Element _element;

        public void Show()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        public void AssignElement(Element element)
        {
            _element = element;

            if (_element.HasTooltip)
            {
                _headerText.text = $"(!) INFO - {_element.ElementName}";
                _descriptionText.text = _element.ElementTooltip;
            }
        }
    }
}