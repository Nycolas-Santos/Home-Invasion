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

            PropertyField fieldName = new PropertyField(name);
            PropertyField fieldDescription = new PropertyField(description);
            PropertyField fieldSprite = new PropertyField(sprite);
            PropertyField fieldColor = new PropertyField(color);

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