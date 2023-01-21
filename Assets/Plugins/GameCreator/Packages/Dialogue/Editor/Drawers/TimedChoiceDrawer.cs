using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(TimedChoice))]
    public class TimedChoiceDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty timedChoice = property.FindPropertyRelative("m_TimedChoice");
            SerializedProperty duration = property.FindPropertyRelative("m_Duration");
            SerializedProperty timeout = property.FindPropertyRelative("m_Timeout");

            PropertyTool fieldTimedChoice = new PropertyTool(timedChoice);
            PropertyTool fieldDuration = new PropertyTool(duration);
            PropertyTool fieldTimeout = new PropertyTool(timeout);
            
            root.Add(fieldTimedChoice);
            root.Add(fieldDuration);
            root.Add(fieldTimeout);

            fieldTimedChoice.EventChange += changeEvent =>
            {
                bool state = changeEvent.changedProperty.boolValue;
                fieldDuration.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
                fieldTimeout.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
            };
            
            bool state = timedChoice.boolValue;
            fieldDuration.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
            fieldTimeout.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
            
            return root;
        }
    }
}