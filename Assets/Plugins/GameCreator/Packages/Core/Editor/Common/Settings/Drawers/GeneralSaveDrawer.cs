using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Common
{
    [CustomPropertyDrawer(typeof(GeneralSave))]
    public class GeneralSaveDrawer : TTitleDrawer
    {
        protected override string Title => "Saving";

        protected override void CreateContent(VisualElement body, SerializedProperty property)
        {
            SerializedProperty system = property.FindPropertyRelative("m_System");
            PropertyElement fieldSave = new PropertyElement(system, "System", false);

            body.Add(fieldSave);
        }
    }
}