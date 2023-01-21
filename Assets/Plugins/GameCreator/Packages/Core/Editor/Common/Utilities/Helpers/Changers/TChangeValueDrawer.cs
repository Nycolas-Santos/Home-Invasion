using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Common
{
    public abstract class TChangeValueDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty operation = property.FindPropertyRelative("m_Operation");
            SerializedProperty value = property.FindPropertyRelative("m_Value");
                
            PropertyTool fieldOperation = new PropertyTool(operation, property.displayName);
            PropertyTool fieldValue = new PropertyTool(value, " ");

            root.Add(fieldOperation);
            root.Add(fieldValue);
            
            return root;
        }
    }
}