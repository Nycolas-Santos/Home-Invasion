using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomEditor(typeof(IndicatorItemUI))]
    public class IndicatorItemUIEditor : UnityEditor.Editor
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
            
            SerializedProperty activeIfOnscreen = this.serializedObject.FindProperty("m_ActiveIfOnscreen");
            SerializedProperty activeIfOffscreen = this.serializedObject.FindProperty("m_ActiveIfOffscreen");
            SerializedProperty rotateTo = this.serializedObject.FindProperty("m_RotateTo");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(activeIfOnscreen));
            root.Add(new PropertyTool(activeIfOffscreen));
            root.Add(new PropertyTool(rotateTo));

            return root;
        }
    }
}