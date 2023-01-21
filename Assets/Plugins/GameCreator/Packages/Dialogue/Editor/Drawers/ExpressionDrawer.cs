using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(Expression))]
    public class ExpressionDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty id = property.FindPropertyRelative("m_Id");
            SerializedProperty sprite = property.FindPropertyRelative("m_Sprite");
            SerializedProperty speechUI = property.FindPropertyRelative("m_OverrideSpeechSkin");

            PropertyTool fieldId = new PropertyTool(id, "ID");
            PropertyTool fieldSprite = new PropertyTool(sprite);
            PropertyTool fieldSpeechUI = new PropertyTool(speechUI, "Speech Skin");
            
            root.Add(fieldId);
            root.Add(fieldSprite);
            root.Add(fieldSpeechUI);
            
            SerializedProperty onStart = property.FindPropertyRelative("m_InstructionsOnStart");
            SerializedProperty onEnd = property.FindPropertyRelative("m_InstructionsOnEnd");

            PropertyTool fieldOnStart = new PropertyTool(onStart);
            PropertyTool fieldOnEnd = new PropertyTool(onEnd);
            
            root.Add(new SpaceSmaller());
            root.Add(new LabelTitle("On Start"));
            root.Add(fieldOnStart);
            
            root.Add(new SpaceSmaller());
            root.Add(new LabelTitle("On End"));
            root.Add(fieldOnEnd);
            
            return root;
        }
    }
}