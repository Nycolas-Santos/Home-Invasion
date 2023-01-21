using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(Value))]
    public class ValueDrawer : PropertyDrawer
    {
        public const string PROPERTY_KEY = "m_Key";
        
        private const string PROPERTY_VALUE = "m_Value";
        private const string PROPERTY_IN_BOLD = "m_InBold";
        private const string PROPERTY_IN_ITALIC = "m_InItalic";
        private const string PROPERTY_USE_COLOR = "m_UseColor";
        private const string PROPERTY_COLOR = "m_Color";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return MakePropertyGUI(property);
        }

        public static VisualElement MakePropertyGUI(SerializedProperty property)
        {
            SerializedProperty key = property.FindPropertyRelative(PROPERTY_KEY);
            SerializedProperty value = property.FindPropertyRelative(PROPERTY_VALUE);

            VisualElement root = new VisualElement();
            
            root.Add(new PropertyTool(key));
            root.Add(new PropertyTool(value));
            
            SerializedProperty inBold = property.FindPropertyRelative(PROPERTY_IN_BOLD);
            SerializedProperty inItalic = property.FindPropertyRelative(PROPERTY_IN_ITALIC);

            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(inBold));
            root.Add(new PropertyTool(inItalic));

            SerializedProperty useColor = property.FindPropertyRelative(PROPERTY_USE_COLOR);
            SerializedProperty color = property.FindPropertyRelative(PROPERTY_COLOR);
            
            PropertyTool fieldUseColor = new PropertyTool(useColor);
            PropertyTool fieldColor = new PropertyTool(color);

            root.Add(new SpaceSmaller());
            root.Add(fieldUseColor);
            root.Add(fieldColor);
            
            fieldUseColor.EventChange += changeEvent =>
            {
                fieldColor.style.display = changeEvent.changedProperty.boolValue
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
            };

            fieldColor.style.display = useColor.boolValue
                ? DisplayStyle.Flex
                : DisplayStyle.None;

            return root;
        }
    }
}