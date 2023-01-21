using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(VolumeCapsule))]
    public class VolumeCapsuleDrawer : VolumeDrawer
    {
        protected override void CreateGUI(SerializedProperty property, VisualElement root)
        {
            SerializedProperty center = property.FindPropertyRelative("m_Center");
            SerializedProperty height = property.FindPropertyRelative("m_Height");
            SerializedProperty radius = property.FindPropertyRelative("m_Radius");
            SerializedProperty direction = property.FindPropertyRelative("m_Direction");

            PropertyTool fieldCenter = new PropertyTool(center);
            PropertyTool fieldHeight = new PropertyTool(height);
            PropertyTool fieldRadius = new PropertyTool(radius);
            PropertyTool fieldDirection = new PropertyTool(direction);

            root.Add(fieldCenter);
            root.Add(fieldHeight);
            root.Add(fieldRadius);
            root.Add(fieldDirection);
        }
    }
}