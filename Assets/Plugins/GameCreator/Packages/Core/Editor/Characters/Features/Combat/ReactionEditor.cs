using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomEditor(typeof(Reaction), true)]
    public class ReactionEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement content = new VisualElement();

            SerializedProperty transitionIn = this.serializedObject.FindProperty("m_TransitionIn");
            SerializedProperty transitionOut = this.serializedObject.FindProperty("m_TransitionOut");
            
            content.Add(new SpaceSmall());
            content.Add(new PropertyTool(transitionIn));
            content.Add(new PropertyTool(transitionOut));
            
            SerializedProperty useRootMotion = this.serializedObject.FindProperty("m_UseRootMotion");
            SerializedProperty speed = this.serializedObject.FindProperty("m_Speed");
            
            content.Add(new SpaceSmall());
            content.Add(new PropertyTool(useRootMotion));
            content.Add(new PropertyField(speed));

            SerializedProperty reactions = this.serializedObject.FindProperty("m_ReactionList");
            
            content.Add(new SpaceSmall());
            content.Add(new PropertyField(reactions));

            return content;
        }
    }
}