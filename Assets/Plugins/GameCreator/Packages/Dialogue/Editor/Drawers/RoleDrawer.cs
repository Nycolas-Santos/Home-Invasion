using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
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

            PropertyTool field = new PropertyTool(target, actor.name);
            root.Add(field);

            return root;
        }
    }
}