using UnityEngine;
using TMPro;
using TopDownRogueLike.Commons;
using UnityEditor.VersionControl;

namespace TopDownRogueLike.Core.Manager
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _notificationContainer;

        [SerializeField]
        private GameObject _notificationTemplatePrefab;

        [SerializeField]
        private TextMeshProUGUI _notificationText;

        private static NotificationManager _instance;

        public static NotificationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<NotificationManager>();
                    if (_instance == null)
                    {
                        Debug.LogError(ConstantVariable.NotificationManagerNotFoundErr);
                    }
                }

                return _instance;
            }
        }

        public void ShowNotification(Sprite sprite, string itemType, int amount)
        {
            Notification notification = Instantiate(_notificationTemplatePrefab, _notificationContainer).GetComponent<Notification>(); ;
            notification.Initialize(sprite, itemType, amount);
        }

        public void ShowNotificationText(string message)
        {
            _notificationText.gameObject.SetActive(true);
            _notificationText.SetText(message);
        }

        public void HideNotificationText()
        {
            _notificationText.gameObject.SetActive(false);
            _notificationText.SetText(string.Empty);
        }
    }
}

