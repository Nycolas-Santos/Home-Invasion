using GameCreator.Editor.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(Typewriter))]
    public class TypewriterDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Typewriter Effect";

        protected override void CreatePropertyContent(VisualElement container, SerializedProperty property)
        {
            SerializedProperty useTypewriter = property.FindPropertyRelative("m_UseTypewriter");
            SerializedProperty frequency = property.FindPropertyRelative("m_Frequency");
            SerializedProperty gibberish = property.FindPropertyRelative("m_Gibberish");
            SerializedProperty pitch = property.FindPropertyRelative("m_Pitch");

            PropertyTool fieldUseTypewriter = new PropertyTool(useTypewriter);
            PropertyTool fieldFrequency = new PropertyTool(frequency);
            PropertyTool fieldGibberish = new PropertyTool(gibberish);
            PropertyTool fieldPitch = new PropertyTool(pitch);

            VisualElement content = new VisualElement();

            container.Add(fieldUseTypewriter);
            container.Add(content);
            content.Add(new SpaceSmall());
            content.Add(fieldFrequency);
            content.Add(new SpaceSmall());
            content.Add(fieldGibberish);
            content.Add(fieldPitch);

            fieldUseTypewriter.EventChange += eventChange =>
            {
                content.SetEnabled(eventChange.changedProperty.boolValue);
            };
            
            content.SetEnabled(useTypewriter.boolValue);
        }
    }
}