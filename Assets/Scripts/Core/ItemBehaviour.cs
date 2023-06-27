using System.Collections.Generic;
using UnityEngine;
using TopDownRogueLike.Commons;
using TopDownRogueLike.Core.Manager;
using TopDownRogueLike.Scriptable;

namespace TopDownRogueLike.Core
{
    public class ItemBehaviour : MonoBehaviour
    {

        [SerializeField]
        private List<ItemBehaviourScriptableObject> _itemBehaviour = new List<ItemBehaviourScriptableObject>();

        private static ItemBehaviour _instance;
        private Dictionary<Item.ItemTypeEnum, ItemBehaviourScriptableObject> _itemBehaviourMap;

        /// <summary>
        /// The instance of the ItemBehaviour.
        /// </summary>
        public static ItemBehaviour Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ItemBehaviour>();
                    if (_instance == null)
                    {
                        Debug.LogError(ConstantVariable.ItemBehaviourNotFoundErr);
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            MakeInstance();
            InitializeSpriteMap();
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

        // <summary>
        /// Initializes the item sprite map dictionary.
        /// </summary>
        private void InitializeSpriteMap()
        {
            _itemBehaviourMap = new Dictionary<Item.ItemTypeEnum, ItemBehaviourScriptableObject>();

            foreach (var itemAsset in _itemBehaviour)
            {
                _itemBehaviourMap[itemAsset.ItemType] = itemAsset;
            }
        }

        /// <summary>
        /// Retrieves the sprite associated with the specified item type.
        /// </summary>
        /// <param name="itemType">The type of the item.</param>
        /// <returns>The sprite associated with the item type, or null if not found.</returns>
        public Sprite GetSpriteByItemType(Item.ItemTypeEnum itemType)
        {
            if (_itemBehaviourMap.TryGetValue(itemType, out ItemBehaviourScriptableObject item))
            {
                return item.ItemSprite;
            }

            return null;
        }

        /// <summary>
        /// Checks if an item type is stackable.
        /// </summary>
        /// <param name="itemType">The type of the item.</param>
        /// <returns><c>true</c> if the item is stackable; otherwise, <c>false</c>.</returns>
        public bool IsStacakable(Item.ItemTypeEnum itemType)
        {
            if (_itemBehaviourMap.TryGetValue(itemType, out ItemBehaviourScriptableObject item))
            {
                return item.IsStackable;
            }

            return false;
        }

        /// <summary>
        /// Checks if an item type can persist in the inventory.
        /// </summary>
        /// <param name="itemType">The type of the item.</param>
        /// <returns><c>true</c> if the item can persist; otherwise, <c>false</c>.</returns>
        public bool CanPersistItem(Item.ItemTypeEnum itemType)
        {
            if (_itemBehaviourMap.TryGetValue(itemType, out ItemBehaviourScriptableObject item))
            {
                return item.CanPersistItem;
            }

            return false;
        }

        /// <summary>
        /// Processes the action associated with an item type.
        /// </summary>
        /// <param name="itemType">The type of the item.</param>
        /// <param name="customAmount">Optional custom amount value for the item action.</param>
        public void ProcessItemAction(Item.ItemTypeEnum itemType, int customAmount = 0)
        {
            switch (itemType)
            {
                case Item.ItemTypeEnum.Coin:
                    IncreaseCoin(customAmount);
                    break;
                case Item.ItemTypeEnum.PoisonPotion:
                    DecreasePlayerHealth();
                    break;
                case Item.ItemTypeEnum.HealthPotion:
                    IncreasePlayerHealth();
                    break;
                default:
                    Debug.LogWarning(ConstantVariable.UnknownItemTypeErr);
                    break;
            }
        }

        /// <summary>
        /// Increases the coin count of the player.
        /// </summary>
        /// <param name="customAmount">Optional custom amount value to increase the coin count.</param>
        private void IncreaseCoin(int customAmount = 0)
        {
            if (_itemBehaviourMap.TryGetValue(Item.ItemTypeEnum.Coin, out ItemBehaviourScriptableObject item))
            {
                int coinValue = customAmount > 0 ? customAmount : item.CoinValue;
                GameManager.Instance.CurrentCoin += coinValue;
            }
        }

        /// <summary>
        /// Decreases the player's health using a poison potion.
        /// </summary>
        private void DecreasePlayerHealth()
        {
            if (_itemBehaviourMap.TryGetValue(Item.ItemTypeEnum.PoisonPotion, out ItemBehaviourScriptableObject item))
            {
                GameManager.Instance.PlayerController.Damage(item.PoisonDamageValue);
            }
        }

        /// <summary>
        /// Increases the player's health using a health potion.
        /// </summary>
        private void IncreasePlayerHealth()
        {
            if (_itemBehaviourMap.TryGetValue(Item.ItemTypeEnum.HealthPotion, out ItemBehaviourScriptableObject item))
            {
                GameManager.Instance.PlayerController.IncreasePlayerHealth(item.HealthPotionValue);
            }
        }
    }
}
