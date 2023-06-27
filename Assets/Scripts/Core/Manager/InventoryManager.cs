using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TopDownRogueLike.Commons;

namespace TopDownRogueLike.Core.Manager
{
    /// <summary>
    /// Manages the inventory system in the game.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [Header("Inventory Item Components")]
        [SerializeField, Tooltip("The parent transform for the inventory items.")]
        private Transform _itemContainer;

        [SerializeField, Tooltip("The template transform for an inventory item.")]
        private Transform _itemTemplate;

        // Events
        public event EventHandler OnItemListChanged;

        // Private fields
        private List<Item> _itemList = new List<Item>();
        private static InventoryManager _instance;
        private ItemBehaviour _itemBehaviour;
        private bool isHovering = false;

        // Constants
        private const string ImageObjectName = "Image";
        private const string TextObjectName = "Text";

        /// <summary>
        /// The instance of the InventoryManager.
        /// </summary>
        public static InventoryManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<InventoryManager>();
                    if (_instance == null)
                    {
                        Debug.LogError(ConstantVariable.InventoryManagerNotFoundErr);
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Indicates if the mouse is hovering over the inventory.
        /// </summary>
        public bool IsHovering { get => isHovering; set => isHovering = value; }
        public ItemBehaviour ItemBehaviour { get => _itemBehaviour; set => _itemBehaviour = value; }

        private void Awake()
        {
            ItemBehaviour = GetComponent<ItemBehaviour>();

            MakeInstance();
        }

        private void Start()
        {
            SetInventory();
        }

        private void MakeInstance()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        /// <summary>
        /// Sets up the inventory system by subscribing to the OnItemListChanged event and refreshing the inventory items.
        /// </summary>
        private void SetInventory()
        {
            OnItemListChanged += Inventory_OnItemListChanged;
            RefreshInventoryItems();
        }

        /// <summary>
        /// Event handler method triggered when the item list in the inventory changes.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The event arguments.</param>
        private void Inventory_OnItemListChanged(object sender, EventArgs e)
        {
            RefreshInventoryItems();
        }

        /// <summary>
        /// Refreshes the inventory items by updating their positions and sprites.
        /// </summary>
        private void RefreshInventoryItems()
        {
            // Clear existing inventory items
            foreach (Transform itemChild in _itemContainer)
            {
                if (itemChild == _itemTemplate)
                {
                    continue;
                }

                Destroy(itemChild.gameObject);
            }

            int x = 0;
            int y = 0;
            float itemContainerCellSize = 80f;

            foreach (Item item in GetItemList())
            {
                // Instantiate and position the inventory item
                RectTransform itemRectTransform = Instantiate(_itemTemplate, _itemContainer).GetComponent<RectTransform>();
                itemRectTransform.gameObject.SetActive(true);
                itemRectTransform.anchoredPosition = new Vector2(x * itemContainerCellSize, y * itemContainerCellSize);

                // Register left click event
                itemRectTransform.GetComponent<ItemUIInteractionHandler>().OnLeftClick += () =>
                {
                    UseItem(item);
                };

                // Register right click event
                itemRectTransform.GetComponent<ItemUIInteractionHandler>().OnRightClick += () =>
                {
                    RemoveItem(item);
                };

                // Set the sprite for the inventory item based on the item type
                Image image = itemRectTransform.Find(ImageObjectName).GetComponent<Image>();
                image.sprite = ItemBehaviour.GetSpriteByItemType(item.ItemType);

                // Set amount of text
                TextMeshProUGUI text = itemRectTransform.Find(TextObjectName).GetComponent<TextMeshProUGUI>();
                DisplayAmountOfText(text, item.Amount);

                x++;

                // If the x value exceeds the maximum number of items in a row, move to the next row
                if (x > ConstantVariable.MAX_ITEM_IN_ITEM_CONTAINER)
                {
                    x = 0;
                    y++;
                }
            }
        }

        /// <summary>
        /// Displays the amount of text based on the current item amount.
        /// </summary>
        /// <param name="text">The TextMeshProUGUI component to update.</param>
        /// <param name="currentAmount">The current amount of the item.</param>
        private void DisplayAmountOfText(TextMeshProUGUI text, int currentAmount)
        {
            if (currentAmount > 1)
            {
                text.SetText(currentAmount.ToString());
            }
            else
            {
                text.SetText(string.Empty);
            }
        }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="itemType">The type of the item to add.</param>
        /// <param name="amount">The amount of the item to add.</param>
        public void AddItem(Item.ItemTypeEnum itemType, int amount)
        {
            var isStackable = ItemBehaviour.Instance.IsStacakable(itemType);
            var newItem = new Item { ItemType = itemType, Amount = amount, IsStackable = isStackable };

            if (isStackable)
            {
                bool itemAlreadyInInventory = false;

                // Check if an item of the same type already exists in the inventory
                foreach (Item item in _itemList)
                {
                    if (item.ItemType == itemType)
                    {
                        // If found, increase the amount of the existing item
                        item.Amount += amount;
                        itemAlreadyInInventory = true;
                    }
                }

                // If the item is not already in the inventory, add it
                if (!itemAlreadyInInventory)
                {
                    _itemList.Add(newItem);
                }
            }
            else
            {
                // If the item cannot be stacked, simply add it to the inventory
                _itemList.Add(newItem);
            }

            // Invoke the event to notify listeners that the item list has changed
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        private void RemoveItem(Item item)
        {
            if (item.IsStackable)
            {
                Item itemInInventory = null;

                // Check if an item of the same type already exists in the inventory
                foreach (Item inventoryItem in _itemList)
                {
                    if (item.ItemType == inventoryItem.ItemType)
                    {
                        // If found, decrease the amount of the existing item
                        inventoryItem.Amount -= item.Amount;
                        itemInInventory = inventoryItem;
                    }
                }

                // If the item is not already in the inventory or the amount is reduced to zero, remove it
                if (itemInInventory != null && itemInInventory.Amount <= 0)
                {
                    _itemList.Remove(itemInInventory);
                }
            }
            else
            {
                // If the item cannot be stacked, simply remove it from the inventory
                _itemList.Remove(item);
            }

            // Invoke the event to notify listeners that the item list has changed
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Uses an item from the inventory.
        /// </summary>
        /// <param name="item">The item to use.</param>
        private void UseItem(Item item)
        {
            // Invoke the item action based on its type
            ItemBehaviour.Instance.ProcessItemAction(item.ItemType);

            // Check the type of the item and perform the corresponding action
            switch (item.ItemType)
            {
                case Item.ItemTypeEnum.Coin:
                    RemoveItem(new Item { ItemType = Item.ItemTypeEnum.Coin, Amount = 1, IsStackable = item.IsStackable });
                    break;
                case Item.ItemTypeEnum.PoisonPotion:
                    RemoveItem(new Item { ItemType = Item.ItemTypeEnum.PoisonPotion, Amount = 1, IsStackable = item.IsStackable });
                    break;
                case Item.ItemTypeEnum.HealthPotion:
                    RemoveItem(new Item { ItemType = Item.ItemTypeEnum.HealthPotion, Amount = 1, IsStackable = item.IsStackable });
                    break;
                default:
                    Debug.LogWarning(ConstantVariable.UnknownItemTypeErr);
                    break;
            }
        }

        /// <summary>
        /// Retrieves the list of items in the inventory.
        /// </summary>
        /// <returns>The list of items in the inventory.</returns>
        public List<Item> GetItemList()
        {
            return _itemList;
        }
    }
}
