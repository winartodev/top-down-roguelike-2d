using UnityEditor;
using TopDownRogueLike.Commons;
using TopDownRogueLike.Scriptable;

namespace TopDownRogueLike.Editor
{
    [CustomEditor(typeof(ItemBehaviourScriptableObject))]
    [CanEditMultipleObjects]

    public class ItemBehaviourScriptableObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty _itemTypeProperty;
        private SerializedProperty _canPersistItemProperty;
        private SerializedProperty _isStackableProperty;
        private SerializedProperty _itemSpriteProperty;
        private SerializedProperty _coinValueProperty;
        private SerializedProperty _healthPotionValueProperty;
        private SerializedProperty _poisonDamageValueProperty;

        private void OnEnable()
        {
            _itemTypeProperty = serializedObject.FindProperty("_itemType");
            _canPersistItemProperty = serializedObject.FindProperty("_canPersistItem");
            _isStackableProperty = serializedObject.FindProperty("_isStackable");
            _itemSpriteProperty = serializedObject.FindProperty("_itemSprite");
            _coinValueProperty = serializedObject.FindProperty("_coinValue");
            _healthPotionValueProperty = serializedObject.FindProperty("_healthPotionValue");
            _poisonDamageValueProperty = serializedObject.FindProperty("_poisonDamageValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Item.ItemTypeEnum itemTypeEnum = (Item.ItemTypeEnum)_itemTypeProperty.enumValueIndex;

            EditorGUILayout.PropertyField(_itemTypeProperty);

            if (itemTypeEnum > 0)
            {
                EditorGUILayout.PropertyField(_itemSpriteProperty);
                EditorGUILayout.PropertyField(_canPersistItemProperty);

                if (_canPersistItemProperty.boolValue)
                {
                    EditorGUILayout.PropertyField(_isStackableProperty);
                }

                HandleDrawItemBehaviour(itemTypeEnum);
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void HandleDrawItemBehaviour(Item.ItemTypeEnum itemTypeEnum)
        {
            switch (itemTypeEnum)
            {
                case Item.ItemTypeEnum.Coin:
                    EditorGUILayout.PropertyField(_coinValueProperty);
                    break;
                case Item.ItemTypeEnum.HealthPotion:
                    EditorGUILayout.PropertyField(_healthPotionValueProperty);
                    break;
                case Item.ItemTypeEnum.PoisonPotion:
                    EditorGUILayout.PropertyField(_poisonDamageValueProperty);
                    break;
                case Item.ItemTypeEnum.Chest:
                    break;
            }
        }
    }
}
