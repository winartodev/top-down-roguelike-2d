using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using TopDownRogueLike.Commons;
using TopDownRogueLike.Core.Manager;

namespace TopDownRogueLike.Core
{
    [Serializable]
    public struct ItemInsideChest
    {
        public GameObject ItemPrefab;
        public int Amount;
    }

    public class ChestInteractor : ItemInteractor
    {
        [Header("Chest Components")]
        [SerializeField, Tooltip("Specifies if the chest is empty.")]
        private bool _isEmptyChest;

        [SerializeField, Tooltip("The list of items inside the chest.")]
        private List<ItemInsideChest> _itemsInsideChest;

        private Animator _animator;
        private bool _isTiggerByPlayer;
        private bool _isClaimed;

        /// <summary>
        /// Determines if the chest can be opened.
        /// </summary>
        private bool CanOpenChest
        {
            get => ItemType == Item.ItemTypeEnum.Chest && !_isClaimed && _isTiggerByPlayer && Input.GetKeyDown(KeyCode.E);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();

            if (_itemsInsideChest.Count == 0 && !_isEmptyChest)
            {
                // The chest is empty if it has no items inside and was not previously marked as empty
                _isEmptyChest = true;
            }
        }

        private void Update()
        {
            // Checks for chest interaction during each frame update.
            CheckChestInteraction();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(ConstantVariable.PLAYER_TAG))
            {
                if (ItemType == Item.ItemTypeEnum.Chest)
                {
                    _isTiggerByPlayer = true;

                    if (!_isClaimed)
                    {
                        NotificationManager.Instance.ShowNotificationText("Press E to Interact with chest");
                    }
                    else
                    {
                        NotificationManager.Instance.ShowNotificationText("Chest already claimed");
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(ConstantVariable.PLAYER_TAG))
            {
                _isTiggerByPlayer = false;
                NotificationManager.Instance.HideNotificationText();
            }
        }

        /// <summary>
        /// Checks if the chest can be opened and opens it if the conditions are met.
        /// </summary>
        private void CheckChestInteraction()
        {
            if (CanOpenChest)
            {
                OpenChest();
            }
        }

        /// <summary>
        /// Opens the chest and handles interactions with the items inside.
        /// </summary>
        private void OpenChest()
        {
            _isClaimed = true;

            StartCoroutine(HandleChestOpening());
        }

        /// <summary>
        /// Handles the opening of a chest and the interactions with the items inside.
        /// </summary>
        private IEnumerator HandleChestOpening()
        {
            AnimationStateManagement.TiggerChestOpen(_animator, _isEmptyChest);

            if (!_isEmptyChest && _itemsInsideChest.Count > 0)
            {
                foreach (var item in _itemsInsideChest)
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
        }
    }
}


