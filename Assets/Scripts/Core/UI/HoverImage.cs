using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameHub.Core.UI
{
    /// <summary>
    /// Class <c>HoverImage</c> adds the ability to change the image background
    /// color when the mouse pointer enters the image area.
    /// area. 
    /// </summary>
    public class HoverImage : Image, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// Instance variable <c>_originalColor</c> stores the original
        /// image background color.
        /// </summary>
        private Color? _originalColor;

        /// <summary>
        /// Instance variable <c>cursorTexture</c> stores the cursor texture to display 
        /// when the mouse pointer enteres the target area.
        /// </summary>
        [SerializeField]
        private Texture2D _cursorTexture;

        [SerializeField]
        /// <summary>
        /// Instance variable <c>hoverColor</c> stores the hover image background color.
        /// </summary>
        private Color _hoverColor;

        /// <summary>
        /// Property <c>HoverColor</c> provides a method method for getting / setting the 
        /// internal hover color.
        /// </summary>
        public Color HoverColor { 
            get
            {
                return _hoverColor;
            }
            set
            {
                _hoverColor = value;
            }
        }

        /// <summary>
        /// Method <c>OnPointerEnter</c> is called when the mouse pointer
        /// enters the target area and the background color is changed
        /// from the default <c>color</c> to the <c>pointerEnterColor</c>.
        /// </summary>
        /// <param name="eventData">
        /// <c>eventData</c> associated with the pointer enter event.
        /// </param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_originalColor == null)
            {
                _originalColor = color;
                color = _hoverColor;

                if (_cursorTexture)
                {
                    Cursor.SetCursor(_cursorTexture, Vector2.zero, CursorMode.Auto);
                }
            }
        }

        /// <summary>
        /// Method <c>OnPointerExit</c> is called when the mouse pointer
        /// exits the target area and the background color is changed
        /// from the <c>pointerEnterColor</c> to the default <c>color</c>.
        /// </summary>
        /// <param name="eventData">
        /// <c>eventData</c> associated with the pointer exit event.
        /// </param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_originalColor != null)
            {
                color = (Color) _originalColor;
                _originalColor = null;

                if (_cursorTexture)
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                }
            }
        }
    }
}