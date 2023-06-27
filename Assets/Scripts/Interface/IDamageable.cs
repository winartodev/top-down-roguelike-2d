using UnityEngine;

namespace TopDownRogueLike.Interface
{
    /// <summary>
    /// Represents an object that can take damage.
    /// </summary>
    public interface IDamageable
    {
        public bool IsAlive { get; }

        /// <summary>
        /// Inflicts damage to the object.
        /// </summary>
        /// <param name="damageTaken">The amount of damage taken.</param>
        void Damage(float damageTaken);

        /// <summary>
        /// Applies knockback to the object.
        /// </summary>
        /// <param name="transform">The transform representing the direction and magnitude of the knockback.</param>
        void Knockback(Transform transform);
    }
}
