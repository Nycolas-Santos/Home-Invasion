using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomEditor(typeof(DialogueUnitChoicesUI))]
    public class DialogueChoicesUIEditor : UnityEditor.Editor
    {
        private VisualElement m_Root;
        
        public override VisualElement CreateInspectorGUI()
        {
            this.m_Root = new VisualElement();

            SerializedProperty active = this.serializedObject.FindProperty("m_Active");
            SerializedProperty content = this.serializedObject.FindProperty("m_ContentChoice");
            SerializedProperty prefab = this.serializedObject.FindProperty("m_PrefabChoice");
                
            this.m_Root.Add(new PropertyTool(active));
            this.m_Root.Add(new SpaceSmall());
            this.m_Root.Add(new PropertyTool(content));
            this.m_Root.Add(new PropertyTool(prefab));

            return this.m_Root;
        }
    }
}