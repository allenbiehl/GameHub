using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

namespace GameHub.Core.UI
{ 
    /// <summary>
    /// Class <c>ExitButton</c> adds the ability to change the background
    /// button color and icon color when the pointer enters the button
    /// and then switches back to the original colors when the pointer
    /// exits the button.
    /// </summary>
    public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// Instance variable <c>_originalButtonColor</c> stores the original
        /// button color before switching to the hover button color.
        /// </summary>
        private Color? _originalButtonColor;

        /// <summary>
        /// Instance variable <c>_originalIconColor</c> stores the original
        /// icon color before switching to the hover icon color.
        /// </summary>
        private Color? _originalIconColor;

        /// <summary>
        /// Instance variable <c>_originalTextColor</c> stores the original
        /// text color before switching to the hover text color.
        /// </summary>
        private Color? _originalTextColor;

        /// <summary>
        /// Instance variable <c>hoverButtonColor</c> is used to store the 
        /// hover button color that will be displayed when the mouse pointer 
        /// enters the target area.
        /// </summary>
        [SerializeField]
        private Color _hoverButtonColor = new Color(0.9056604f, 0.2355693f, 0.2355693f, 1);

        /// <summary>
        /// Instance variables <c>hoverIconColor</c> is used to store the 
        /// hover icon color that will be displayed when the mouse pointer 
        /// enters the target area.
        /// </summary>
        [SerializeField]
        private Color _hoverIconColor = new Color(1, 1, 1, 1);

        /// <summary>
        /// Instance variables <c>hoverTextColor</c> is used to store the 
        /// hover text color that will be displayed when the mouse pointer 
        /// enters the target area.
        /// </summary>
        [SerializeField]
        private Color _hoverTextColor = new Color(1, 1, 1, 1);

        /// <summary>
        /// Instance variable <c>_icon</c> is used to store the icon image
        /// reference that sits on top of the button.
        /// </summary>
        [SerializeField]
        private Image _icon;

        /// <summary>
        /// Instance variable <c>_text</c> is used to store the text
        /// reference that sits on top of the button.
        /// </summary>
        [SerializeField]
        private Text _text;

        /// <summary>
        /// Method <c>OnPointerEnter</c> is called when the mouse pointer
        /// enters the target area and changes all components from their
        /// original color to their hover color.
        /// </summary>
        /// <param name="eventData">
        /// <c>eventData</c> associated with the pointer enter event.
        /// </param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_originalButtonColor == null)
            {
                Image button = transform.GetComponent<Image>();
                
                if (button)
                { 
                    _originalButtonColor = button.color;
                    button.color = _hoverButtonColor;

                    if (_icon)
                    {
                        _originalIconColor = _icon.color;
                        _icon.color = _hoverIconColor;
                    }
                    if (_text)
                    {
                        _originalTextColor = _text.color;
                        _text.color = _hoverTextColor;
                    }
                }
            }
        }

        /// <summary>
        /// Method <c>OnPointerEnter</c> is called when the mouse pointer
        /// exits the target area and changes all components from their
        /// hover color to their original color.
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_originalButtonColor != null)
            {
                Image button = transform.GetComponent<Image>();

                if (button)
                {
                    button.color = (Color) _originalButtonColor;
                    _originalButtonColor = null;

                    if (_icon)
                    {
                        _icon.color = (Color) _originalIconColor;
                        _originalIconColor = null;
                    }
                    if (_text)
                    {
                        _text.color = (Color)_originalTextColor;
                        _originalTextColor = null;
                    }
                }
            }
        }
    }
}