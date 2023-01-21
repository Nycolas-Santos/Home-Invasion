using GameCreator.Editor.Common;
using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(Info))]
    public class InfoDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Info";

        protected override void CreatePropertyContent(VisualElement container, SerializedProperty property)
        {
            SerializedProperty name = property.FindPropertyRelative("m_Name");
            SerializedProperty description = property.FindPropertyRelative("m_Description");
            SerializedProperty sprite = property.FindPropertyRelative("m_Sprite");
            SerializedProperty color = property.FindPropertyRelative("m_Color");

            var fieldName = new GameCreator.Editor.Common.PropertyTool(name);
            var fieldDescription = new GameCreator.Editor.Common.PropertyTool(description);
            var fieldSprite = new GameCreator.Editor.Common.PropertyTool(sprite);
            var fieldColor = new GameCreator.Editor.Common.PropertyTool(color);

            container.Add(fieldName);
            container.Add(new SpaceSmall());
            container.Add(fieldDescription);
            container.Add(new SpaceSmall());
            container.Add(fieldSprite);
            container.Add(new SpaceSmall());
            container.Add(fieldColor);
        }
    }
}