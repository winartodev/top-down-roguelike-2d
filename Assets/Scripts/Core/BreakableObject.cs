using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TopDownRogueLike.Interface;

namespace TopDownRogueLike.Core
{
    [Serializable]
    public struct ItemInsideBreakableObject
    {
        public GameObject ItemPrefab;
        public int Amount;
    }

    /// <summary>
    /// Represents a breakable object that can be damaged and broken.
    /// </summary>
    public class BreakableObject : ItemInteractor, IDamageable
    {
        [Header("Breakable Object Components")]
        [SerializeField, Tooltip("Specifies if the object can be broken.")]
        private bool _canBreakable;

        [SerializeField, Tooltip("The strength of the breakable object.")]
        private float _breakableObjectStrength;

        [SerializeField, Tooltip("Specifies if the breakable object is empty.")]
        private bool _isEmptyBreakableObject;

        [SerializeField, Tooltip("The list of items inside the breakable object.")]
        private List<ItemInsideBreakableObject> _listOfItemInsideBreakableObject;

        [SerializeField, Tooltip("The UI GameObject for the health bar.")]
        private GameObject _healthBarUI;

        private HealthBar _healthBar;

        /// <summary>
        /// Determines if the breakable object is still alive.
        /// </summary>
        public bool IsAlive { get => _breakableObjectStrength > 0; }

        private void Awake()
        {
            if (_canBreakable)
            {
                _healthBar = GetComponent<HealthBar>();
            }
        }

        private void Start()
        {
            if (_canBreakable)
            {
                _healthBarUI.SetActive(true);
                _healthBar.SetMaxHealth(_breakableObjectStrength);
            }
            else
            {
                _healthBarUI.SetActive(false);
            }
        }

        /// <summary>
        /// Damages the breakable object by the specified amount.
        /// </summary>
        /// <param name="damageTaken">The amount of damage taken.</param>
        public void Damage(float damageTaken)
        {
            if (!_canBreakable)
            {
                return;
            }

            if (IsAlive)
            {
                _breakableObjectStrength -= damageTaken;
                _healthBar.SetHealth(_breakableObjectStrength);
            }
            else
            {
                StartCoroutine(HandleGiveItem());
            }
        }

        /// <summary>
        /// Applies knockback to the breakable object.
        /// </summary>
        /// <param name="transform">The transform to apply the knockback to.</param>
        public void Knockback(Transform transform)
        {
            Debug.LogWarning("Not Implement");
        }

        /// <summary>
        /// Handles give item when player sucessfully break the object
        /// </summary>
        private IEnumerator HandleGiveItem()
        {
            if (!_isEmptyBreakableObject && _listOfItemInsideBreakableObject.Count > 0)
            {
                foreach (var item in _listOfItemInsideBreakableObject)
                {
                    if (item.ItemPrefab.TryGetComponent(out ItemInteractor itemInteractor))
                    {
                        yield return new WaitForSeconds(0.25f);

                        ShowItemNotification(itemInteractor, item.Amount);

                        if (ItemBehaviour.Instance.CanPersistItem(itemInteractor.ItemType))
                        {
                            AddItemToInventory(itemInteractor.ItemType, item.Amount);
                        }
                        else
                        {
                            ProcessItemAction(itemInteractor.ItemType, item.Amount);
                        }
                    }
                }
            }

            Destroy(transform.parent.gameObject);
        }
    }
}

