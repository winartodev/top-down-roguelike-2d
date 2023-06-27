using UnityEngine;
using TopDownRogueLike.Commons;
using TopDownRogueLike.Interface;

namespace TopDownRogueLike.Core
{
    /// <summary>
    /// Represents the area where the player's attack can damage enemies.
    /// </summary>
    [RequireComponent(typeof(PolygonCollider2D))]
    public class AttackArea : MonoBehaviour
    {
        // Serialized fields
        [SerializeField]
        private PlayerController _playerController;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(ConstantVariable.ENEMY_TAG))
            {
                if (collision.TryGetComponent(out IDamageable damageable))
                {
                    // Damage the enemy by calling the Damage method on the IDamageable component
                    damageable.Damage(_playerController.GetWeaponStrength());
                    damageable.Knockback(transform);
                }
            }

            if (collision.CompareTag(ConstantVariable.BREAKABLE_OBJECT_TAG))
            {
                if (collision.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(_playerController.GetWeaponStrength());
                }
            }
        }
    }
}