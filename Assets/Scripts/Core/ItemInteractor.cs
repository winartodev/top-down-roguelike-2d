using UnityEngine;
using TopDownRogueLike.Commons;
using TopDownRogueLike.Core.Manager;

namespace TopDownRogueLike.Core
{
    /// <summary>
    /// Handles item interactions and chest opening functionality.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Animator))]
    public class ItemInteractor : MonoBehaviour
    {
        // Serialized fields
        [Header("Item Components")]
        [SerializeField, Tooltip("The type of item.")]
        private Item.ItemTypeEnum _itemType;

        [SerializeField, Tooltip("The amount of items.")]
        private int _itemAmount;

        public Item.ItemTypeEnum ItemType { get => _itemType; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(ConstantVariable.PLAYER_TAG))
            {

                if (_itemType != Item.ItemTypeEnum.Chest && _itemType != Item.ItemTypeEnum.BreakableObject)
                {
                    HandleItemInteraction();
                }
            }
        }

        /// <summary>
        /// Handles the interaction with a non-chest item.
        /// </summary>
        private void HandleItemInteraction()
        {
            if (ItemBehaviour.Instance.CanPersistItem(_itemType))
            {
                AddItemToInventory(_itemType, _itemAmount);
            }
            else
            {
                ProcessItemAction(_itemType);
            }

            // Destroy the object after interaction
            Destroy(gameObject);
        }

        /// <summary>
        /// Adds the item from the chest to the player's inventory.
        /// </summary>
        /// <param name="itemType">The type of the item.</param>
        /// <param name="amount">The amount of the item.</param>
        protected static void AddItemToInventory(Item.ItemTypeEnum itemType, int amount)
        {
            InventoryManager.Instance.AddItem(itemType, amount);
        }

        /// <summary>
        /// Processes the action of the item.
        /// </summary>
        protected void ProcessItemAction(Item.ItemTypeEnum itemType, int customAmount = 0)
        {
            ItemBehaviour.Instance.ProcessItemAction(itemType, customAmount);
        }

        /// <summary>
        /// Shows a notification for the item contained within the chest.
        /// </summary>
        /// <param name="itemInteractor">The item interactor component representing the item.</param>
        /// <param name="amount">The amount of the item.</param>
        protected static void ShowItemNotification(ItemInteractor itemInteractor, int amount)
        {
            Sprite _itemSprite = InventoryManager.Instance.ItemBehaviour.GetSpriteByItemType(itemInteractor.ItemType);

            NotificationManager.Instance.ShowNotification(_itemSprite, itemInteractor.ItemType.ToString(), amount);
        }
    }
}
