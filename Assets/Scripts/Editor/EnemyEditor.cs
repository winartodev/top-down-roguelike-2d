using UnityEditor;
using UnityEngine;
using TopDownRogueLike.Core;

namespace TopDownRogueLike.Editor
{
    [CustomEditor(typeof(Enemy), true)]
    [CanEditMultipleObjects]
    public class EnemyEditor : UnityEditor.Editor
    {
        private SerializedProperty _enemyHealthProperty;
        private SerializedProperty _enemyDamageProperty;
        private SerializedProperty _canEnemyMoveProperty;
        private SerializedProperty _movementSpeedProperty;
        private SerializedProperty _enemyRangeProperty;

        private void OnSceneGUI()
        {
            Enemy enemy = (Enemy)target;
            Handles.color = Color.red;
            Handles.DrawWireArc(enemy.transform.position, Vector3.forward, Vector3.right, 360, enemy.EnemyRange);
        }

        private void OnEnable()
        {
            // Find the serialized properties for each aspect of the enemy's behavior
            _enemyHealthProperty = serializedObject.FindProperty("_enemyHealth");
            _canEnemyMoveProperty = serializedObject.FindProperty("_canEnemyMove");
            _movementSpeedProperty = serializedObject.FindProperty("_movementSpeed");
            _enemyRangeProperty = serializedObject.FindProperty("_enemyRange");
            _enemyDamageProperty = serializedObject.FindProperty("_enemyDamage");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_enemyHealthProperty);

            EditorGUILayout.PropertyField(_enemyDamageProperty);

            EditorGUILayout.PropertyField(_canEnemyMoveProperty);

            bool canEnemyMove = _canEnemyMoveProperty.boolValue;

            if (canEnemyMove)
            {
                EditorGUILayout.PropertyField(_movementSpeedProperty);
                EditorGUILayout.PropertyField(_enemyRangeProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
