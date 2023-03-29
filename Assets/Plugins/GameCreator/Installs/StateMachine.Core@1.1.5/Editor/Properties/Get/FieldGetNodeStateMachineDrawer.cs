using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using NinjutsuGames.StateMachine.Runtime;
using NinjutsuGames.StateMachine.Runtime.Variables;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NinjutsuGames.StateMachine.Editor
{
    [CustomPropertyDrawer(typeof(FieldGetNodeStateMachine))]
    public class FieldGetNodeStateMachineDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            
            var stateMachine = property.FindPropertyRelative("m_StateMachine");
            var nodeName = property.FindPropertyRelative("m_Name");
            var nodeId = property.FindPropertyRelative("m_GUID");

            var fieldVariable = new ObjectField(stateMachine.displayName)
            {
                allowSceneObjects = false,
                objectType = typeof(StateMachineAsset),
                bindingPath = stateMachine.propertyPath
            };

            // var typeIDStr = typeID.FindPropertyRelative(IdStringDrawer.NAME_STRING);
            // var typeIDValue = new IdString(typeIDStr.stringValue);
            
            var toolPickName = new StateMachinePickNodeTool(
                fieldVariable, 
                property,
                nodeName.stringValue,
                nodeId.stringValue,
                true
            );

            root.Add(fieldVariable);
            root.Add(toolPickName);
            
            _ = new AlignLabel(root);
            
            property.serializedObject.Update();
            if(StateMachineAsset.Active != null && stateMachine.objectReferenceValue == null && stateMachine.objectReferenceValue != StateMachineAsset.Active)
            {
                stateMachine.objectReferenceValue = StateMachineAsset.Active;
                SerializationUtils.ApplyUnregisteredSerialization(property.serializedObject);
            }

            return root;
        }
    }
}