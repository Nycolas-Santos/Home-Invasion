using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(Ingredient))]
    public class IngredientDrawer : PropertyDrawer
    {
        public const string PROP_ITEM = "m_Item";
        public const string PROP_AMOUNT = "m_Amount";

        // PAINT METHODS: -------------------------------------------------------------------------
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty item = property.FindPropertyRelative(PROP_ITEM);
            SerializedProperty amount = property.FindPropertyRelative(PROP_AMOUNT);

            var fieldItem = new GameCreator.Editor.Common.PropertyTool(item);
            var fieldAmount = new GameCreator.Editor.Common.PropertyTool(amount);

            root.Add(fieldItem);
            root.Add(fieldAmount);
            
            return root;
        }
    }
}