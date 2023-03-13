using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomEditor(typeof(Class))]
    public class ClassEditor : UnityEditor.Editor
    {
        private const string USS_PATH = EditorPaths.PACKAGES + "Stats/Editor/StyleSheets/Class";
        
        private const string CLASS_LABEL = "gc-stats-class-label";
        
        // PAINT METHODS: -------------------------------------------------------------------------
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            StyleSheet[] sheets = StyleSheetUtils.Load(USS_PATH);
            foreach (StyleSheet sheet in sheets) root.styleSheets.Add(sheet);
            
            SerializedProperty className = this.serializedObject.FindProperty("m_Class");
            SerializedProperty classDesc = this.serializedObject.FindProperty("m_Description");
            
            root.Add(new PropertyTool(className));
            root.Add(new PropertyTool(classDesc));
            
            Label labelAttributes = new Label("Attributes:");
            Label labelStats = new Label("Stats:");
            
            labelAttributes.AddToClassList(CLASS_LABEL);
            labelStats.AddToClassList(CLASS_LABEL);
            
            SerializedProperty attributes = this.serializedObject.FindProperty("m_Attributes");
            SerializedProperty stats = this.serializedObject.FindProperty("m_Stats");

            PropertyField fieldAttributes = new PropertyField(attributes);
            PropertyField fieldStats = new PropertyField(stats);

            root.Add(labelAttributes);
            root.Add(fieldAttributes);
            root.Add(labelStats);
            root.Add(fieldStats);

            return root;
        }
    }
}