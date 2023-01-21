using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomEditor(typeof(DialogueCoordinatesUI))]
    public class DialogueCoordinatesUIEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;
        
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            SerializedProperty alignX = this.serializedObject.FindProperty("m_DefaultVertical");
            SerializedProperty alignY = this.serializedObject.FindProperty("m_DefaultHorizontal");
            
            this.m_Root.Add(new PropertyTool(alignX));
            this.m_Root.Add(new PropertyTool(alignY));
            
            SerializedProperty offset = this.serializedObject.FindProperty("m_Offset");
            SerializedProperty keepParent = this.serializedObject.FindProperty("m_KeepInParent");
            
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(offset));
            this.m_Root.Add(new PropertyTool(keepParent));
            
            SerializedProperty anchorTo = this.serializedObject.FindProperty("m_AnchorTo");
            SerializedProperty anchor = this.serializedObject.FindProperty("m_Anchor");

            PropertyTool anchorToField = new PropertyTool(anchorTo);
            PropertyTool anchorField = new PropertyTool(anchor)
            {
                style =
                {
                    display = anchorTo.enumValueIndex == 0
                        ? DisplayStyle.None
                        : DisplayStyle.Flex
                }
            };

            anchorToField.EventChange += changeEvent =>
            {
                anchorField.style.display = changeEvent.changedProperty.enumValueIndex == 0
                    ? DisplayStyle.None
                    : DisplayStyle.Flex;
            };

            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(anchorToField);
            this.m_Root.Add(anchorField);

            return this.m_Root;
        }
    }
}