using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomEditor(typeof(DialogueUnitTimerUI))]
    public class DialogueTimerUIEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;

        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();
            
            SerializedProperty active = this.serializedObject.FindProperty("m_Active");
            SerializedProperty timer = this.serializedObject.FindProperty("m_TimerBar");
            SerializedProperty invert = this.serializedObject.FindProperty("m_InvertProgress");
            SerializedProperty remainingA = this.serializedObject.FindProperty("m_RemainingSeconds");
            SerializedProperty remainingB = this.serializedObject.FindProperty("m_RemainingDecimals");
            SerializedProperty durationA = this.serializedObject.FindProperty("m_DurationSeconds");
            SerializedProperty durationB = this.serializedObject.FindProperty("m_DurationDecimals");

            this.m_Root.Add(new PropertyTool(active));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(timer));
            this.m_Root.Add(new PropertyTool(invert));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(remainingA));
            this.m_Root.Add(new PropertyTool(remainingB));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(durationA));
            this.m_Root.Add(new PropertyTool(durationB));

            return this.m_Root;
        }
    }
}