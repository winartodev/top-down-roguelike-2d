namespace TopDownRogueLike.Commons
{
    /// <summary>
    /// Represents an item in the game.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The type of the item.
        /// </summary>
        public enum ItemTypeEnum
        {
            None = 0,
            Coin,
            PoisonPotion,
            HealthPotion,
            Chest,
            BreakableObject
        }

        private ItemTypeEnum _itemType;
        private int _amount;
        private bool _isStackable;

        /// <summary>
        /// The type of the item.
        /// </summary>
        public ItemTypeEnum ItemType { get => _itemType; set => _itemType = value; }

        /// <summary>
        /// The amount of the item.
        /// </summary>
        public int Amount { get => _amount; set => _amount = value; }

        public bool IsStackable { get => _isStackable; set => _isStackable = value; }
    }
}