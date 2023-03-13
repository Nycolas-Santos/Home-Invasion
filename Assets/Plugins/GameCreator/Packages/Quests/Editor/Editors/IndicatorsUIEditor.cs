using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomEditor(typeof(IndicatorsUI))]
    public class IndicatorsUIEditor : UnityEditor.Editor
    {
        // PAINT METHODS: -------------------------------------------------------------------------

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty camera = this.serializedObject.FindProperty("m_Camera");
            SerializedProperty layers = this.serializedObject.FindProperty("m_Layers");

            root.Add(new PropertyTool(camera));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(layers));

            SerializedProperty prefab = this.serializedObject.FindProperty("m_Prefab");
            SerializedProperty content = this.serializedObject.FindProperty("m_Content");

            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(prefab));
            root.Add(new PropertyTool(content)); 
            
            SerializedProperty showHidden = this.serializedObject.FindProperty("m_HiddenQuests");
            SerializedProperty hideUntrack = this.serializedObject.FindProperty("m_HideUntracked");
            SerializedProperty keepBounds = this.serializedObject.FindProperty("m_KeepInBounds");

            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(showHidden));
            root.Add(new PropertyTool(hideUntrack));
            root.Add(new PropertyTool(keepBounds));

            return root;
        }
    }
}