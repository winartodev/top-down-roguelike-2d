using System.Collections;
using UnityEngine;
using TopDownRogueLike.Interface;
using TopDownRogueLike.Commons;

namespace TopDownRogueLike.Core
{
    [RequireComponent(typeof(Animator), typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        // Serialized fields
        [Header("Enemy Components")]
        [SerializeField, Tooltip("The health of the enemy.")]
        private float _enemyHealth;

        [SerializeField, Tooltip("The amount of damage the enemy deals.")]
        private int _enemyDamage;

        [SerializeField, Tooltip("If checked, the enemy is allowed to move.")]
        private bool _canEnemyMove;

        [SerializeField, Tooltip("The movement positions for the enemy.")]
        [Range(0f, 10f)]
        private float _movementSpeed;

        [SerializeField, Tooltip("The range to enemies can following the player")]
        [Range(0, 10f)]
        private float _enemyRange;

        [SerializeField, Tooltip("The positions defining the enemy's movement.")]
        private MovePosition _movePosition;

        // Private fields
        private Animator _animator;
        private HealthBar _healthBar;
        private GameObject _player;
        private Vector3 _initialPos;

        // Property for checking if the enemy is alive
        public bool IsAlive { get => _enemyHealth > 0; }
        public float EnemyRange { get => _enemyRange; set => _enemyRange = value; }

        private void Awake()
        {
            // Obtain component references
            _animator = GetComponent<Animator>();
            _healthBar = GetComponent<HealthBar>();
            _player = GameObject.FindGameObjectWithTag(ConstantVariable.PLAYER_TAG);
            _initialPos = transform.position;
        }

        private void Start()
        {
            // Set maximum health
            _healthBar.SetMaxHealth(_enemyHealth);
        }

        private void Update()
        {
            if (_player != null)
            {
                if (Vector3.Distance(_player.transform.position, transform.position) <= _enemyRange)
                {
                    FollowPlayer();
                }
                else
                {
                    MoveToInitialPos();
                }
            }
        }

        private void FollowPlayer()
        {
            AnimationStateManagement.EnemyAnimationStateRunning(_animator, true);
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _movementSpeed * Time.deltaTime);
        }

        private void MoveToInitialPos()
        {
            transform.position = Vector3.MoveTowards(transform.position, _initialPos, _movementSpeed * Time.deltaTime);

            if (_initialPos == transform.position)
            {
                AnimationStateManagement.EnemyAnimationStateRunning(_animator, false);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(ConstantVariable.PLAYER_TAG))
            {
                if (collision.gameObject.TryGetComponent(out IDamageable component))
                {
                    // Damage the player by calling the Damage method on the IDamageable component
                    component.Damage(_enemyDamage);
                    component.Knockback(transform);
                }
            }
        }

        public void Damage(float damageTaken)
        {
            if (IsAlive)
            {
                // Reduce enemy health and update health bar
                _enemyHealth -= damageTaken;
                _healthBar.SetHealth(_enemyHealth);
            }
            else
            {
                if (_movePosition != null)
                {
                    // Destroy associated move position object
                    Destroy(_movePosition.gameObject);
                }

                // Destroy the enemy game object
                Destroy(gameObject);
            }
        }

        public void Knockback(Transform transform)
        {
            Vector2 difference = this.transform.position - transform.position;
            this.transform.position = new Vector2(this.transform.position.x + difference.x, this.transform.position.y + difference.y);
        }
    }
}