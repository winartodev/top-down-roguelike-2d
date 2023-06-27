using UnityEngine;

namespace TopDownRogueLike.Core
{
    /// <summary>
    /// Represents a weapon in the game.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class Weapon : MonoBehaviour
    {
        // Serialized fields
        [Header("Weapon Components")]
        [SerializeField] private int _weaponDamage;

        /// <summary>
        /// The damage value of the weapon.
        /// </summary>
        public int WeaponDamage => _weaponDamage;
    }
}
