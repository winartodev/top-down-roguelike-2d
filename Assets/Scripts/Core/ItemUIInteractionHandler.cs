using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TopDownRogueLike.Core.Manager;

namespace TopDownRogueLike.Core
{
    /// <summary>
    /// Handles the interaction events for an item UI element.
    /// </summary>
    public class ItemUIInteractionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action OnLeftClick = null;
        public event Action OnRightClick = null;

        /// <summary>
        /// Called when the cursor enters the UI element.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            InventoryManager.Instance.IsHovering = true;
        }

        /// <summary>
        /// Called when the cursor exits the UI element.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerExit(PointerEventData eventData)
        {
            InventoryManager.Instance.IsHovering = false;
        }

        /// <summary>
        /// Called when a pointer button is clicked on the UI element.
        /// </summary>
        /// <param name="eventData">The pointer event data.</param>
        public void OnPointerClick(PointerEventData eventData)
        {
            InventoryManager.Instance.IsHovering = false;

            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnLeftClick?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    OnRightClick?.Invoke();
                    break;
            }
        }
    }
}

