using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(Story))]
    public class StoryDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty content = property.FindPropertyRelative("m_Content");
            PropertyTool fieldContent = new PropertyTool(content);
            
            root.Add(fieldContent);

            return root;
        }
    }
}