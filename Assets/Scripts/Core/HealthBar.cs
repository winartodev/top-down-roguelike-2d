using UnityEngine;
using UnityEngine.UI;

namespace TopDownRogueLike.Core
{
    /// <summary>
    /// Represents a health bar UI component.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        // Serialized fields
        [Header("Health Bar Components")]
        [SerializeField, Tooltip("The slider component representing the health bar.")]
        private Slider _slider;

        [SerializeField, Tooltip("The fill image of the health bar.")]
        private Image _fill;

        [SerializeField, Tooltip("The gradient used to color the health bar fill.")]
        private Gradient _gradient;

        [SerializeField, Tooltip("Determines if the health bar should use a world camera for positioning.")]
        private bool _useWorldCamera = true;

        [SerializeField, Tooltip("The canvas containing the health bar UI.")]
        private Canvas _canvas;

        private void Start()
        {
            if (_useWorldCamera)
            {
                _canvas.worldCamera = Camera.main;
            }
        }

        /// <summary>
        /// Sets the maximum health value for the health bar.
        /// </summary>
        /// <param name="health">The maximum health value.</param>
        public void SetMaxHealth(float health)
        {
            _slider.maxValue = health;
            _slider.value = health;

            // Evaluate the gradient color at 100% to set the initial fill color
            _fill.color = _gradient.Evaluate(1f);
        }

        /// <summary>
        /// Sets the current health value for the health bar.
        /// </summary>
        /// <param name="health">The current health value.</param>
        public void SetHealth(float health)
        {
            _slider.value = health;

            // Evaluate the gradient color based on the normalized value of the slider
            _fill.color = _gradient.Evaluate(_slider.normalizedValue);
        }
    }
}
