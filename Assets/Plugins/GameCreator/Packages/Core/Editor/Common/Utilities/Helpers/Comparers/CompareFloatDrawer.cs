using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Common
{
    [CustomPropertyDrawer(typeof(CompareDouble))]
    public class CompareFloatDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty comparison = property.FindPropertyRelative("m_Comparison");
            SerializedProperty compareTo = property.FindPropertyRelative("m_CompareTo");
            
            PropertyTool fieldComparison = new PropertyTool(comparison);
            PropertyTool fieldCompareTo = new PropertyTool(compareTo, property.displayName);

            root.Add(fieldComparison);
            root.Add(fieldCompareTo);
            
            return root;
        }
    }
}