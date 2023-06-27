using UnityEngine;
using TopDownRogueLike.Commons;

namespace TopDownRogueLike.Scriptable
{
    /// <summary>
    /// Represents a scriptable object for defining item behaviors in a top-down rogue-like game.
    /// </summary>
    /// <remarks>
    /// This scriptable object contains various properties and attributes that define the behavior
    /// of different types of items in the game, such as coins, health potions, and poison potions.
    /// </remarks>
    [CreateAssetMenu(fileName = "ItemBehaviourScriptableObject", menuName = "Top Down Rouge Like / Item Behaviour Scriptabl eObject", order = 1)]
    public class ItemBehaviourScriptableObject : ScriptableObject
    {
        [Header("Item Components")]
        [SerializeField, Tooltip("The type of the item.")]
        private Item.ItemTypeEnum _itemType;

        [SerializeField, Tooltip("The sprite representing the item.")]
        private Sprite _itemSprite;

        [Header("Persistent Attributes")]
        [SerializeField, Tooltip("Specifies if the item can persist across scenes.")]
        private bool _canPersistItem;

        [SerializeField, Tooltip("Specifies if the item can be stacked in the inventory.")]
        private bool _isStackable;

        [Header("Coin Behaviour")]
        [SerializeField, Tooltip("The value of the coin item.")]
        private int _coinValue;

        [Header("Health Potion Behaviour")]
        [SerializeField, Tooltip("The amount of health restored by the health potion item.")]
        private int _healthPotionValue;

        [Header("Poison Potion Behaviour")]
        [SerializeField, Tooltip("The amount of damage inflicted by the poison potion item.")]
        private int _poisonDamageValue;

        public Item.ItemTypeEnum ItemType { get => _itemType; }
        public bool CanPersistItem { get => _canPersistItem; }
        public bool IsStackable { get => _isStackable; }
        public Sprite ItemSprite { get => _itemSprite; }
        public int CoinValue { get => _coinValue; }
        public int HealthPotionValue { get => _healthPotionValue; }
        public int PoisonDamageValue { get => _poisonDamageValue; }
    }
}
