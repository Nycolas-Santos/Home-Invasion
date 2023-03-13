using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(AttributeData))]
    public class AttributeDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty minValue = property.FindPropertyRelative("m_MinValue");
            SerializedProperty maxValue = property.FindPropertyRelative("m_MaxValue");
            SerializedProperty startPercent = property.FindPropertyRelative("m_StartPercent");
            
            PropertyTool fieldMinValue = new PropertyTool(minValue);
            PropertyTool fieldMaxValue = new PropertyTool(maxValue);
            PropertyTool fieldStartPercent = new PropertyTool(startPercent);
            
            root.Add(fieldMinValue);
            root.Add(fieldMaxValue);
            root.Add(fieldStartPercent);
            
            return root;
        }
    }
}