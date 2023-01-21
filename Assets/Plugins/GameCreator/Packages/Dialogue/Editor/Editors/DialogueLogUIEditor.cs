using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomEditor(typeof(DialogueLogUI))]
    public class DialogueLogUIEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;

        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();
            
            SerializedProperty actor = this.serializedObject.FindProperty("m_ActiveActor");
            SerializedProperty actorName = this.serializedObject.FindProperty("m_ActorName");
            SerializedProperty actorDesc = this.serializedObject.FindProperty("m_ActorDescription");
            SerializedProperty activePortrait = this.serializedObject.FindProperty("m_ActivePortrait");
            SerializedProperty portrait = this.serializedObject.FindProperty("m_Portrait");
            SerializedProperty text = this.serializedObject.FindProperty("m_Text");
            
            this.m_Root.Add(new PropertyTool(actor));
            this.m_Root.Add(new PropertyTool(actorName));
            this.m_Root.Add(new PropertyTool(actorDesc));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(activePortrait));
            this.m_Root.Add(new PropertyTool(portrait));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(text));

            return this.m_Root;
        }
    }
}