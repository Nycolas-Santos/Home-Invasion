using GameCreator.Editor.Common;
using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(Shape))]
    public class ShapeDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Shape";

        protected override void CreatePropertyContent(VisualElement container, SerializedProperty property)
        {
            SerializedProperty width = property.FindPropertyRelative("m_Width");
            SerializedProperty height = property.FindPropertyRelative("m_Height");
            SerializedProperty weight = property.FindPropertyRelative("m_Weight");
            SerializedProperty maxStack = property.FindPropertyRelative("m_MaxStack");

            var fieldWidth = new GameCreator.Editor.Common.PropertyTool(width);
            var fieldHeight = new GameCreator.Editor.Common.PropertyTool(height);
            var fieldWeight = new GameCreator.Editor.Common.PropertyTool(weight);
            var fieldMaxStack = new GameCreator.Editor.Common.PropertyTool(maxStack);

            container.Add(fieldWidth);
            container.Add(fieldHeight);
            container.Add(new SpaceSmall());
            container.Add(fieldWeight);
            container.Add(fieldMaxStack);
            
            Item item = maxStack.serializedObject.targetObject as Item;
            fieldMaxStack.SetEnabled(item != null && item.Sockets.ListLength == 0);
        }
    }
}