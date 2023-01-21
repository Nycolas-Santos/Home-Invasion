using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(VolumeSphere))]
    public class VolumeSphereDrawer : VolumeDrawer
    {
        protected override void CreateGUI(SerializedProperty property, VisualElement root)
        {
            SerializedProperty center = property.FindPropertyRelative("m_Center");
            SerializedProperty radius = property.FindPropertyRelative("m_Radius");

            PropertyTool fieldCenter = new PropertyTool(center);
            PropertyTool fieldRadius = new PropertyTool(radius);

            root.Add(fieldCenter);
            root.Add(fieldRadius);
        }
    }
}