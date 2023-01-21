using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(Actant))]
    public class ActantDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty name = property.FindPropertyRelative("m_Name");
            SerializedProperty description = property.FindPropertyRelative("m_Description");

            PropertyTool fieldName = new PropertyTool(name);
            PropertyTool fieldDescription = new PropertyTool(description);

            root.Add(fieldName);
            root.Add(fieldDescription);

            return root;
        }
    }
}