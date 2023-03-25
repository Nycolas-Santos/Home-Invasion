using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(OverrideStatData))]
    public class OverrideStatDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            VisualElement head = new VisualElement();
            VisualElement body = new VisualElement();

            SerializedProperty changeBase = property.FindPropertyRelative("m_ChangeBase");
            SerializedProperty baseValue = property.FindPropertyRelative("m_Base");

            PropertyField fieldChangeBase = new PropertyField(changeBase);
            PropertyField fieldBase = new PropertyField(baseValue);
            
            fieldChangeBase.RegisterValueChangeCallback(changeEvent =>
            {
                body.Clear();
                if (!changeEvent.changedProperty.boolValue) return;
                
                body.Add(fieldBase);
                fieldBase.Bind(baseValue.serializedObject);
            });
            
            head.Add(fieldChangeBase);
            if (changeBase.boolValue) body.Add(fieldBase);
            
            root.Add(head);
            root.Add(body);
            
            return root;
        }
    }
}