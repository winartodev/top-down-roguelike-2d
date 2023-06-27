using UnityEditor;
using TopDownRogueLike.Core;

namespace TopDownRogueLike.Editor
{
    [CustomEditor(typeof(HealthBar), true)]
    [CanEditMultipleObjects]
    public class HelathBarEditor : UnityEditor.Editor
    {
        private SerializedProperty _sliderProperty;
        private SerializedProperty _fillProperty;
        private SerializedProperty _gradientProperty;
        private SerializedProperty _useWorldCameraProperty;
        private SerializedProperty _canvasProperty;

        private void OnEnable()
        {
            // Find the serialized properties for each aspect of the health bar
            _sliderProperty = serializedObject.FindProperty("_slider");
            _fillProperty = serializedObject.FindProperty("_fill");
            _gradientProperty = serializedObject.FindProperty("_gradient");
            _useWorldCameraProperty = serializedObject.FindProperty("_useWorldCamera");
            _canvasProperty = serializedObject.FindProperty("_canvas");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_sliderProperty);

            EditorGUILayout.PropertyField(_fillProperty);

            EditorGUILayout.PropertyField(_gradientProperty);

            EditorGUILayout.PropertyField(_useWorldCameraProperty);

            bool useWorldCamera = _useWorldCameraProperty.boolValue;

            if (useWorldCamera)
            {
                EditorGUILayout.PropertyField(_canvasProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

