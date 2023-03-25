using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(AttributeItem))]
    public class AttributeItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty attr = property.FindPropertyRelative("m_Attribute");
            SerializedProperty changeStartPercent = property.FindPropertyRelative("m_ChangeStartPercent");
            SerializedProperty startPercent = property.FindPropertyRelative("m_StartPercent");

            VisualElement contentStartPercent = new VisualElement();

            PropertyField fieldAttr = new PropertyField(attr);
            PropertyField fieldChangeStartPercent = new PropertyField(changeStartPercent);
            PropertyField fieldStartPercent = new PropertyField(startPercent, " ");

            fieldChangeStartPercent.RegisterValueChangeCallback(changeEvent =>
            {
                contentStartPercent.Clear();
                if (changeEvent.changedProperty.boolValue)
                {
                    contentStartPercent.Add(fieldStartPercent);
                    fieldStartPercent.Bind(changeEvent.changedProperty.serializedObject);
                }
            });
            
            root.Add(fieldAttr);
            root.Add(fieldChangeStartPercent);
            root.Add(contentStartPercent);

            if (changeStartPercent.boolValue) contentStartPercent.Add(fieldStartPercent);

            return root;
        }
    }
}