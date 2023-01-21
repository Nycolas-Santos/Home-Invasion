using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(StockData))]
    public class StockDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty item = property.FindPropertyRelative("m_Item");
            SerializedProperty amount = property.FindPropertyRelative("m_Amount");

            var fieldItem = new GameCreator.Editor.Common.PropertyTool(item);
            var fieldAmount = new GameCreator.Editor.Common.PropertyTool(amount);
            
            root.Add(fieldItem);
            root.Add(fieldAmount);
            
            return root;
        }
    }
}