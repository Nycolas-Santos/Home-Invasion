using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomEditor(typeof(MinimapItemUI))]
    public class MinimapItemUIEditor : UnityEditor.Editor
    {
        // PAINT METHODS: -------------------------------------------------------------------------
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty text = this.serializedObject.FindProperty("m_Text");
            SerializedProperty sprite = this.serializedObject.FindProperty("m_Sprite");
            SerializedProperty color = this.serializedObject.FindProperty("m_Color");
            
            root.Add(new PropertyTool(text));
            root.Add(new PropertyTool(sprite));
            root.Add(new PropertyTool(color));

            return root;
        }
    }
}