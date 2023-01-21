using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomEditor(typeof(DialogueUnitPortraitsUI))]
    public class DialoguePortraitsUIEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;

        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();
            
            SerializedProperty active = this.serializedObject.FindProperty("m_Active");
            
            SerializedProperty showListeners = this.serializedObject.FindProperty("m_ShowListeners");
            SerializedProperty listenersTint = this.serializedObject.FindProperty("m_ListenerTint");

            SerializedProperty active1 = this.serializedObject.FindProperty("m_ActivePrimary");
            SerializedProperty portrait1 = this.serializedObject.FindProperty("m_PrimaryPortrait");
            SerializedProperty actorName1 = this.serializedObject.FindProperty("m_PrimaryActorName");
            SerializedProperty actorDesc1 = this.serializedObject.FindProperty("m_PrimaryActorDescription");
            
            SerializedProperty active2 = this.serializedObject.FindProperty("m_ActiveAlternate");
            SerializedProperty portrait2 = this.serializedObject.FindProperty("m_AlternatePortrait");
            SerializedProperty actorName2 = this.serializedObject.FindProperty("m_AlternateActorName");
            SerializedProperty actorDesc2 = this.serializedObject.FindProperty("m_AlternateActorDescription");

            SerializedProperty clipOnChange = this.serializedObject.FindProperty("m_ClipOnChange");
            
            this.m_Root.Add(new PropertyTool(active));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(showListeners));
            this.m_Root.Add(new PropertyTool(listenersTint));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(active1));
            this.m_Root.Add(new PropertyTool(portrait1));
            this.m_Root.Add(new PropertyTool(actorName1));
            this.m_Root.Add(new PropertyTool(actorDesc1));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(active2));
            this.m_Root.Add(new PropertyTool(portrait2));
            this.m_Root.Add(new PropertyTool(actorName2));
            this.m_Root.Add(new PropertyTool(actorDesc2));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(clipOnChange));
            
            return this.m_Root;
        }
    }
}