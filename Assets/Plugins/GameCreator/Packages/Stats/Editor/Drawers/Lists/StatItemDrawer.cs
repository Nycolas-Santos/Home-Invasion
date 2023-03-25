using GameCreator.Editor.Common;
using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomPropertyDrawer(typeof(StatItem))]
    public class StatItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty stat = property.FindPropertyRelative("m_Stat");
            SerializedProperty changeValue = property.FindPropertyRelative("m_ChangeBase");
            SerializedProperty value = property.FindPropertyRelative("m_Base");
            SerializedProperty changeFormula = property.FindPropertyRelative("m_ChangeFormula");
            SerializedProperty formula = property.FindPropertyRelative("m_Formula");

            VisualElement contentValue = new VisualElement();
            VisualElement contentFormula = new VisualElement();
            
            PropertyField fieldStat = new PropertyField(stat);
            PropertyField fieldChangeValue = new PropertyField(changeValue);
            PropertyField fieldValue = new PropertyField(value, " ");
            PropertyField fieldChangeFormula = new PropertyField(changeFormula);
            PropertyField fieldFormula = new PropertyField(formula, " ");

            fieldChangeValue.RegisterValueChangeCallback(changeEvent =>
            {
                contentValue.Clear();
                if (changeEvent.changedProperty.boolValue)
                {
                    contentValue.Add(fieldValue);
                    fieldValue.Bind(changeEvent.changedProperty.serializedObject);
                }
            });
            
            fieldChangeFormula.RegisterValueChangeCallback(changeEvent =>
            {
                contentFormula.Clear();
                if (changeEvent.changedProperty.boolValue)
                {
                    contentFormula.Add(fieldFormula);
                    fieldFormula.Bind(changeEvent.changedProperty.serializedObject);
                }
            });
            
            root.Add(fieldStat);
            
            root.Add(fieldChangeValue);
            root.Add(contentValue);
            root.Add(fieldChangeFormula);
            root.Add(contentFormula);
            
            if (changeValue.boolValue) contentValue.Add(fieldValue);
            if (changeFormula.boolValue) contentFormula.Add(fieldFormula);

            return root;
        }
    }
}