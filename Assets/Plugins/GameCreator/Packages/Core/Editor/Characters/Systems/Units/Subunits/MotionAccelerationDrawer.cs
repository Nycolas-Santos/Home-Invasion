using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(MotionAcceleration))]
    public class MotionAccelerationDrawer : TBoxDrawer
    {
        protected override string Name(SerializedProperty property) => "Acceleration";

        protected override void CreatePropertyContent(VisualElement container, SerializedProperty property)
        {
            container.Clear();

            SerializedProperty useAcceleration = property.FindPropertyRelative("m_UseAcceleration");
            SerializedProperty acceleration = property.FindPropertyRelative("m_Acceleration");
            SerializedProperty deceleration = property.FindPropertyRelative("m_Deceleration");

            PropertyTool fieldUseAcceleration = new PropertyTool(useAcceleration);
            PropertyTool fieldAcceleration = new PropertyTool(acceleration);
            PropertyTool fieldDeceleration = new PropertyTool(deceleration);
            
            fieldUseAcceleration.EventChange += _ =>
            {
                property.serializedObject.ApplyModifiedProperties();
                property.serializedObject.Update();
                
                Refresh(container, useAcceleration.boolValue, fieldAcceleration, fieldDeceleration);
            };
            
            container.Add(fieldUseAcceleration);
            Refresh(container, useAcceleration.boolValue, fieldAcceleration, fieldDeceleration);

            fieldUseAcceleration.Bind(property.serializedObject);
            fieldAcceleration.Bind(property.serializedObject);
            fieldDeceleration.Bind(property.serializedObject);
        }

        private static void Refresh(VisualElement container, bool isActive, params PropertyTool[] fields)
        {
            foreach (PropertyTool field in fields)
            {
                if (!container.Contains(field)) continue;
                container.Remove(field);
            }

            if (!isActive) return;
            
            foreach (PropertyTool field in fields)
            {
                container.Add(field);
            }
        }
    }
}