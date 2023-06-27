using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TopDownRogueLike.Commons
{
    [RequireComponent(typeof(Animator))]
    public class Notification : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TextMeshProUGUI _text;

        private const float timeToDelay = 5f;

        private void Start()
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(DestroySelf());
            }
        }

        public void Initialize(Sprite iconSprite, string ItemName, int amount)
        {
            _icon.sprite = iconSprite;
            _text.SetText($"You Got\n{amount} {ItemName}");
        }

        private IEnumerator DestroySelf()
        {
            yield return new WaitForSeconds(timeToDelay);
            Destroy(gameObject);
        }
    }
}
