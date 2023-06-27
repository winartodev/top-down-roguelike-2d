using UnityEngine;
using TopDownRogueLike.Commons;
using TopDownRogueLike.Core.Manager;
using TopDownRogueLike.Interface;

namespace TopDownRogueLike.Core
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Animator))]
    public class PlayerController : MonoBehaviour, IDamageable
    {
        // Serialized fields
        [Header("Player Components")]
        [SerializeField, Tooltip("The health of the player.")]
        private float _playerHealth;

        [SerializeField, Tooltip("The movement positions for the player.")]
        [Range(0f, 10f)] private float _movementSpeed;

        [SerializeField, Tooltip("The game object that holds the weapon.")]
        private GameObject _weaponHolder;

        [SerializeField, Tooltip("The collider used for the attack area.")]
        private GameObject _attackAreaCollider2D;

        // Private fields
        private HealthBar _healthBar;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private bool _isFacingRight = true;
        private bool _isAttack = false;
        private float _time;

        // Property for checking if the player is alive
        public bool IsAlive { get => _playerHealth > 0; }

        // Awake is called when the script instance is being loaded.
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _healthBar = GetComponent<HealthBar>();
        }

        private void Start()
        {
            _healthBar.SetMaxHealth(_playerHealth);
        }

        // Update is called once per frame.
        private void Update()
        {
            HandleMovementInput();
            HandleAttackInput();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Damage(10);
            }
        }

        /// <summary>
        /// Handle player movement input.
        /// </summary>
        private void HandleMovementInput()
        {
            // Retrieves horizontal and vertical input values
            float _horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left, 1 is right
            float _vertical = Input.GetAxisRaw("Vertical"); // -1 is down, 1 is up

            // Flip the player's sprite based on movement direction
            FlipSprite(_horizontal);

            // Calculate movement vector and apply velocity to the rigidbody
            Vector2 movement = new Vector2(_horizontal, _vertical).normalized * _movementSpeed;
            _rigidbody2D.velocity = movement;

            // Determine if the player is running and update the animator state
            bool isRunning = (_horizontal != 0 || _vertical != 0);
            AnimationStateManagement.PlayerAnimationStateRunning(_animator, isRunning);
        }

        /// <summary>
        /// Handles player attack input.
        /// </summary>
        private void HandleAttackInput()
        {
            if (Input.GetMouseButtonDown(0) && !InventoryManager.Instance.IsHovering)
            {
                Attack();
            }

            // Will handle attack duration time out
            if (_isAttack)
            {
                _time += Time.deltaTime;
                if (_time >= 0.3f)
                {
                    _time = 0;
                    _isAttack = false;
                    _attackAreaCollider2D.SetActive(false);
                }
            }
        }

        public void Damage(float damageTaken)
        {
            AnimationStateManagement.PlayerGotDamage(_animator);

            if (IsAlive)
            {
                // Reduce enemy health and update health bar
                _playerHealth -= damageTaken;
                _healthBar.SetHealth(_playerHealth);
            }
            else
            {
                GameManager.Instance.GameOver();
                // Destroy the enemy game object
                Destroy(gameObject);
            }
        }

        public void Knockback(Transform transform)
        {
            Vector2 difference = this.transform.position - transform.position;
            this.transform.position = new Vector2(this.transform.position.x + difference.x, this.transform.position.y + difference.y);
        }

        /// <summary>
        /// Flips the player sprite horizontally to face the opposite direction.
        /// </summary>
        /// <param name="horizontalInput">The horizontal input value.</param>
        private void FlipSprite(float horizontalInput)
        {
            // Flip the player sprite based on movement direction
            if (horizontalInput > 0 && !_isFacingRight || horizontalInput < 0 && _isFacingRight)
            {
                _isFacingRight = !_isFacingRight;

                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        /// <summary>
        /// Triggers the player attack animation.
        /// </summary>
        private void Attack()
        {
            _isAttack = true;
            _attackAreaCollider2D.SetActive(true);
            AnimationStateManagement.PlayerAttack(_animator);
        }

        /// <summary>
        /// Checks if the weapon holder has any child objects.
        /// </summary>
        /// <returns>Returns true if the weapon holder has child objects, otherwise false.</returns>
        private bool WeaponHolderHaveChild()
        {
            if (_weaponHolder.transform.childCount > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retrieves the strength of the equipped weapon by returning the damage value.
        /// </summary>
        /// <returns>The amount of damage dealt by the weapon.</returns>
        public int GetWeaponStrength()
        {
            if (WeaponHolderHaveChild())
            {
                Weapon _weapon = _weaponHolder.GetComponentInChildren<Weapon>();
                return _weapon.WeaponDamage;
            }

            return 0;
        }

        public void IncreasePlayerHealth(int health)
        {
            _playerHealth += health;
            _healthBar.SetHealth(_playerHealth);
        }
    }
}