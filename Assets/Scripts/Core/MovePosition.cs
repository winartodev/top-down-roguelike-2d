using UnityEngine;

namespace TopDownRogueLike.Core
{
    /// <summary>
    /// Represents a movement from a starting position to an ending position.
    /// </summary>
    public class MovePosition : MonoBehaviour
    {
        // Serialized fields
        [SerializeField, Tooltip("The starting position for the movement.")]
        private Transform _startPosition;

        [SerializeField, Tooltip("The ending position for the movement.")]
        private Transform _endPosition;

        /// <summary>
        /// The starting position for the movement.
        /// </summary>
        public Transform StartPosition => _startPosition;

        /// <summary>
        /// The ending position for the movement.
        /// </summary>
        public Transform EndPosition => _endPosition;
    }
}
