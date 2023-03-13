using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(StatData))]
    public class StatDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty value = property.FindPropertyRelative("m_Base");
            SerializedProperty formula = property.FindPropertyRelative("m_Formula");
            
            PropertyTool fieldValue = new PropertyTool(value, "Base (value)");
            PropertyTool fieldFormula = new PropertyTool(formula);
            
            root.Add(fieldValue);
            root.Add(fieldFormula);
            
            return root;
        }
    }
}