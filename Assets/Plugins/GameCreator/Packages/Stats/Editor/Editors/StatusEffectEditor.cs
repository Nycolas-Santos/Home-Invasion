using GameCreator.Runtime.Stats;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Stats
{
    [CustomEditor(typeof(StatusEffect))]
    public class StatusEffectEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty id = this.serializedObject.FindProperty("m_ID");
            SerializedProperty data = this.serializedObject.FindProperty("m_Data");
            SerializedProperty info = this.serializedObject.FindProperty("m_Info");

            PropertyField fieldID = new PropertyField(id);
            PropertyField fieldData = new PropertyField(data);
            PropertyField fieldInfo = new PropertyField(info);

            root.Add(fieldID);
            root.Add(fieldData);
            root.Add(fieldInfo);

            SerializedProperty onStart = this.serializedObject.FindProperty("m_OnStart");
            SerializedProperty onEnd = this.serializedObject.FindProperty("m_OnEnd");
            SerializedProperty whileActive = this.serializedObject.FindProperty("m_WhileActive");

            PropertyField fieldOnStart = new PropertyField(onStart);
            PropertyField fieldOnEnd = new PropertyField(onEnd);
            PropertyField fieldWhileActive = new PropertyField(whileActive);
            
            root.Add(fieldOnStart);
            root.Add(fieldOnEnd);
            root.Add(fieldWhileActive);

            return root;
        }
    }
}