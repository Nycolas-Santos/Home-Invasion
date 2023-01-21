using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomEditor(typeof(DialogueChoiceUI))]
    public class DialogueChoiceUIEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;

        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();
            
            SerializedProperty text = this.serializedObject.FindProperty("m_Text");
            SerializedProperty index = this.serializedObject.FindProperty("m_Index");
            SerializedProperty button = this.serializedObject.FindProperty("m_Button");
            SerializedProperty actor = this.serializedObject.FindProperty("m_Actor");
            SerializedProperty actorName = this.serializedObject.FindProperty("m_ActorName");
            SerializedProperty actorDesc = this.serializedObject.FindProperty("m_ActorDescription");

            SerializedProperty activeSelected = this.serializedObject.FindProperty("m_ActiveSelected");
            SerializedProperty activeCondition = this.serializedObject.FindProperty("m_ActiveCondition");

            SerializedProperty graphic = this.serializedObject.FindProperty("m_Graphic");
            SerializedProperty graphicNormal = this.serializedObject.FindProperty("m_GraphicNormal");
            SerializedProperty graphicSelected = this.serializedObject.FindProperty("m_GraphicSelected");
            
            SerializedProperty color = this.serializedObject.FindProperty("m_Color");
            SerializedProperty colorNormal = this.serializedObject.FindProperty("m_ColorNormal");
            SerializedProperty colorVisited = this.serializedObject.FindProperty("m_ColorVisited");

            this.m_Root.Add(new PropertyTool(text));
            this.m_Root.Add(new PropertyTool(index));
            this.m_Root.Add(new PropertyTool(button));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(actor));
            this.m_Root.Add(new PropertyTool(actorName));
            this.m_Root.Add(new PropertyTool(actorDesc));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(activeSelected));
            this.m_Root.Add(new PropertyTool(activeCondition));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(graphic));
            this.m_Root.Add(new PropertyTool(graphicNormal));
            this.m_Root.Add(new PropertyTool(graphicSelected));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(color));
            this.m_Root.Add(new PropertyTool(colorNormal));
            this.m_Root.Add(new PropertyTool(colorVisited));

            return this.m_Root;
        }
    }
}