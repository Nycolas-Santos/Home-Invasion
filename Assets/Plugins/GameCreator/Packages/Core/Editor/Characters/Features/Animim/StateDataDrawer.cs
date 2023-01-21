using System;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(StateData))]
    public class StateDataDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            VisualElement head = new VisualElement();
            VisualElement body = new VisualElement();
            
            root.Add(head);
            root.Add(body);
            
            SerializedProperty propertyStateType = property.FindPropertyRelative("m_StateType");
            PropertyTool fieldStateType = new PropertyTool(propertyStateType);

            head.Add(fieldStateType);
            
            this.Refresh(body, property, propertyStateType.enumValueIndex);
            fieldStateType.EventChange += eventChange =>
            {
                this.Refresh(body, property, eventChange.changedProperty.enumValueIndex);
            };
            
            return root;
        }

        private void Refresh(VisualElement container, SerializedProperty property, int enumIndex)
        {
            container.Clear();

            SerializedProperty propertyClip = property.FindPropertyRelative("m_AnimationClip");
            SerializedProperty propertyRTC = property.FindPropertyRelative("m_RuntimeController");
            SerializedProperty propertyState = property.FindPropertyRelative("m_State");
            SerializedProperty propertyAvatarMask = property.FindPropertyRelative("m_AvatarMask");
            
            PropertyTool fieldClip = new PropertyTool(propertyClip);
            PropertyTool fieldRTC = new PropertyTool(propertyRTC);
            PropertyTool fieldState = new PropertyTool(propertyState);
            PropertyTool fieldAvatarMask = new PropertyTool(propertyAvatarMask);
            
            switch ((StateData.StateType)enumIndex)
            {
                case StateData.StateType.AnimationClip:
                    container.Add(fieldClip);
                    container.Add(fieldAvatarMask);
                    break;
                
                case StateData.StateType.RuntimeController:
                    container.Add(fieldRTC);
                    container.Add(fieldAvatarMask);
                    break;
                
                case StateData.StateType.State:
                    container.Add(fieldState);
                    break;
            }
            
            container.Bind(property.serializedObject);
        }
    }
}
