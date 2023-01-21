using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Common
{
    [CustomPropertyDrawer(typeof(IdString))]
    public class IdStringDrawer : PropertyDrawer
    {
        public const string NAME_STRING = "m_String";
        public const string NAME_HASH = "m_Hash";
        
        // PAINT METHODS: -------------------------------------------------------------------------
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty value = property.FindPropertyRelative(NAME_STRING);
            PropertyTool field = new PropertyTool(value, property.displayName);
            
            field.EventChange += changeEvent =>
            {
                string text = changeEvent.changedProperty.stringValue;
                value.stringValue = TextUtils.ProcessID(text, false);
                
                property.serializedObject.ApplyModifiedProperties();
                property.serializedObject.Update();
            };

            return field;
        }
    }
}