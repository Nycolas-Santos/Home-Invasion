using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(OverrideAttributeData))]
    public class OverrideAttributeDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            VisualElement head = new VisualElement();
            VisualElement body = new VisualElement();

            SerializedProperty changePercent = property.FindPropertyRelative("m_ChangeStartPercent");
            SerializedProperty percent = property.FindPropertyRelative("m_StartPercent");

            PropertyField fieldChangePercent = new PropertyField(changePercent);
            PropertyField fieldPercent = new PropertyField(percent);
            
            fieldChangePercent.RegisterValueChangeCallback(changeEvent =>
            {
                body.Clear();
                if (!changeEvent.changedProperty.boolValue) return;
                
                body.Add(fieldPercent);
                fieldPercent.Bind(percent.serializedObject);
            });
            
            head.Add(fieldChangePercent);
            if (changePercent.boolValue) body.Add(fieldPercent);
            
            root.Add(head);
            root.Add(body);
            
            return root;
        }
    }
}