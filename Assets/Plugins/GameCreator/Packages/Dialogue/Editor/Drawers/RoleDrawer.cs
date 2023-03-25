using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(Role))]
    public class RoleDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty actor = property.FindPropertyRelative("m_Actor");
            SerializedProperty target = property.FindPropertyRelative("m_Target");

            PropertyField field = new PropertyField(target, actor.name);
            root.Add(field);

            return root;
        }
    }
}