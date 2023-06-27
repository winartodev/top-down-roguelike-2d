using UnityEditor;
using TopDownRogueLike.Commons;
using TopDownRogueLike.Core;

namespace TopDownRogueLike.Editor
{
    [CustomEditor(typeof(ItemInteractor), true)]
    [CanEditMultipleObjects]
    public class ItemInteractorEditor : UnityEditor.Editor
    {
        Item.ItemTypeEnum itemTypeEnum;

        // commons item interactor properties
        private SerializedProperty _itemTypeProperty;
        private SerializedProperty _itemAmountProperty;

        // chest properties
        private SerializedProperty _isEmptyChestProperty;
        private SerializedProperty _itemsInsideChestProperty;

        // breakable object properties
        private SerializedProperty _canBreakableProperty;
        private SerializedProperty _breakableObjectStrength;
        private SerializedProperty _listOfItemInsideBreakableObjectProperty;
        private SerializedProperty _isEmptyBreakableObjectProperty;
        private SerializedProperty _healthBarUIProperty;

        private bool CanShowItemAmountProperty
        {
            get => itemTypeEnum > 0 && itemTypeEnum != Item.ItemTypeEnum.Chest && itemTypeEnum != Item.ItemTypeEnum.BreakableObject;
        }

        private void OnEnable()
        {
            // Find the serialized properties for each aspect of the object interactor
            _itemTypeProperty = serializedObject.FindProperty("_itemType");
            _itemAmountProperty = serializedObject.FindProperty("_itemAmount");

            _isEmptyChestProperty = serializedObject.FindProperty("_isEmptyChest");
            _itemsInsideChestProperty = serializedObject.FindProperty("_itemsInsideChest");

            _canBreakableProperty = serializedObject.FindProperty("_canBreakable");
            _breakableObjectStrength = serializedObject.FindProperty("_breakableObjectStrength");
            _isEmptyBreakableObjectProperty = serializedObject.FindProperty("_isEmptyBreakableObject");
            _listOfItemInsideBreakableObjectProperty = serializedObject.FindProperty("_listOfItemInsideBreakableObject");
            _healthBarUIProperty = serializedObject.FindProperty("_healthBarUI");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            itemTypeEnum = (Item.ItemTypeEnum)_itemTypeProperty.enumValueIndex;

            EditorGUILayout.PropertyField(_itemTypeProperty);

            if (CanShowItemAmountProperty)
            {
                EditorGUILayout.PropertyField(_itemAmountProperty);
            }

            DrawCustomEditorGUILayoutByItemType(itemTypeEnum);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawCustomEditorGUILayoutByItemType(Item.ItemTypeEnum itemType)
        {
            switch (itemType)
            {
                case Item.ItemTypeEnum.Chest:
                    DrawChestEditorGUI();
                    break;
                case Item.ItemTypeEnum.BreakableObject:
                    DrawBreakableObjectEditorGUI();
                    break;
            }
        }

        private void DrawChestEditorGUI()
        {
            EditorGUILayout.PropertyField(_isEmptyChestProperty);

            if (!_isEmptyChestProperty.boolValue)
            {
                EditorGUILayout.PropertyField(_itemsInsideChestProperty);
            }
        }

        private void DrawBreakableObjectEditorGUI()
        {
            EditorGUILayout.PropertyField(_canBreakableProperty);

            if (_canBreakableProperty.boolValue)
            {
                EditorGUILayout.PropertyField(_breakableObjectStrength);
                EditorGUILayout.PropertyField(_healthBarUIProperty);
                EditorGUILayout.PropertyField(_isEmptyBreakableObjectProperty);

                if (!_isEmptyBreakableObjectProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(_listOfItemInsideBreakableObjectProperty);
                }
            }
        }
    }
}
