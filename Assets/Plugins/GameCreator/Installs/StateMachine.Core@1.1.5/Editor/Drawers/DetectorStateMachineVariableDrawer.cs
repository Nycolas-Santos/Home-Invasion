using System;
using GameCreator.Editor.Common;
using GameCreator.Editor.Variables;
using NinjutsuGames.StateMachine.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace NinjutsuGames.StateMachine.Editor
{
    [CustomPropertyDrawer(typeof(DetectorStateMachine))]

    public class DetectorStateMachineVariableDrawer : TDetectorStateMachineVariableDrawer
    {
        protected override Type AssetType => typeof(StateMachineAsset);
        protected override bool AllowSceneReferences => false;
        protected override bool AutoSelectReference => true;

        protected override TNamePickTool Tool(ObjectField field, SerializedProperty property)
        {
            property.serializedObject.Update();
            if(StateMachineAsset.Active != null && field.value == null && field.value != StateMachineAsset.Active)
            {
                field.value = StateMachineAsset.Active;
                SerializationUtils.ApplyUnregisteredSerialization(property.serializedObject);
            }
            return new StateMachinePickTool(field, property);
        }
    }
}