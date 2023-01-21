using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(Acting))]
    public class ActingDrawer : PropertyDrawer
    {
        private const string PROPERTY_PORTRAIT = "m_Portrait";
        
        internal const string PROPERTY_ACTOR = "m_Actor";
        internal const string PROPERTY_EXPRESSION = "m_Expression";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            SerializedProperty portrait = property.FindPropertyRelative(PROPERTY_PORTRAIT);
            
            root.Add(new PropertyTool(portrait));
            root.Add(new ExpressingTool(property));
            
            return root;
        }
    }
}