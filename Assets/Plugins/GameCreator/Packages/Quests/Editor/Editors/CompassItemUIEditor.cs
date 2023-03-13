using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomEditor(typeof(CompassItemUI))]
    public class CompassItemUIEditor : UnityEditor.Editor
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
            
            SerializedProperty group = this.serializedObject.FindProperty("m_CanvasGroup");
            SerializedProperty alpha = this.serializedObject.FindProperty("m_Alpha");

            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(group));
            root.Add(new PropertyTool(alpha));

            return root;
        }
    }
}