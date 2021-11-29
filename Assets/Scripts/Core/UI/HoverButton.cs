using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;
using GameHub.Core.Util;

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
        /// Property <c>hoverButtonColor</c> is used to store the hover button
        /// color that will be displayed when the mouse pointer enters the 
        /// target area.
        /// </summary>
        public Color hoverButtonColor = new Color(0.9056604f, 0.2355693f, 0.2355693f, 1);

        /// <summary>
        /// Property <c>hoverIconColor</c> is used to store the hover icon
        /// color that will be displayed when the mouse pointer enters the 
        /// target area.
        /// </summary>
        public Color hoverTextColor = new Color(1, 1, 1, 1);

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
                Image button = ComponentUtil.GetComponent<Image>(this);
                
                if (button)
                { 
                    _originalButtonColor = button.color;
                    button.color = hoverButtonColor;

                    Image icon = ComponentUtil.FindComponent<Image>("Icon", this);
                    Text text = ComponentUtil.FindComponent<Text>("Text", this);

                    if (icon)
                    {
                        _originalIconColor = icon.color;
                        icon.color = hoverTextColor;
                    }
                    if (text)
                    {
                        _originalTextColor = text.color;
                        text.color = hoverTextColor;
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
                Image button = ComponentUtil.GetComponent<Image>(this);

                if (button)
                {
                    button.color = (Color) _originalButtonColor;
                    _originalButtonColor = null;

                    Image icon = ComponentUtil.FindComponent<Image>("Icon", this);
                    Text text = ComponentUtil.FindComponent<Text>("Text", this);

                    if (icon)
                    {
                        icon.color = (Color) _originalIconColor;
                        _originalIconColor = null;
                    }
                    if (text)
                    {
                        text.color = (Color) _originalTextColor;
                        _originalTextColor = null;
                    }
                }
            }
        }
    }
}