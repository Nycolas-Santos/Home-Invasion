using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(StatusEffectInstruction))]
    public class StatusEffectInstructionDrawer : TBoxDrawer
    {
        protected override void CreatePropertyContent(VisualElement container, SerializedProperty property)
        {
            SerializedProperty instructions = property.FindPropertyRelative("m_Instructions");
            container.Add(new PropertyField(instructions));
        }
    }
}