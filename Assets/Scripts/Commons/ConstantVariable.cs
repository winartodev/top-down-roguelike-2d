namespace TopDownRogueLike.Commons
{
    /// <summary>
    /// A static class that holds constant variables used in the game.
    /// </summary>
    public static class ConstantVariable
    {
        // Tags
        public const string PLAYER_TAG = "Player";
        public const string ENEMY_TAG = "Enemy";
        public const string BREAKABLE_OBJECT_TAG = "BreakableObject";

        // Animations state

        public const string ENEMY_RUNNING = "IsRunning";

        public const string PLAYER_RUNNING = "IsRunning";
        public const string PLAYER_TAKEN_DAMAGE = "IsTakeDamage";
        public const string PLAYER_ATTACK = "IsAttack";
        public const string PLAYER_HIT = "IsHit";

        public const string CHEST_OPEN_WITH_FULL_ITEMS = "FullOpen";
        public const string CHEST_OPEN_WITH_EMPTY_ITEMS = "EmptyOpen";

        // Commons constant values
        public const int MAX_ITEM_IN_ITEM_CONTAINER = 10;


        // Error message
        public const string ItemBehaviourNotFoundErr = "ItemBehaviour instance not found in the scene.";
        public const string GameManagerNotFoundErr = "GameManager instance not found in the scene.";
        public const string InventoryManagerNotFoundErr = "InventoryManager instance not found in the scene.";
        public const string NotificationManagerNotFoundErr = "NotificationManager instance not found in the scene.";

        public const string UnknownItemTypeErr = "Unknown item type";
    }
}
