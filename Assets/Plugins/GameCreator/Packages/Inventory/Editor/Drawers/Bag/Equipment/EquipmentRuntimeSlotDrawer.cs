using System;
using GameCreator.Editor.Characters;
using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(EquipmentRuntimeSlot))]
    public class EquipmentRuntimeSlotDrawer : PropertyDrawer
    {
        private const string PROP_BASE = "m_Override";
        private const string PROP_BONE = "m_OverrideBone";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return CreatePropertyGUI(property, "Bone");
        }

        public static VisualElement CreatePropertyGUI(SerializedProperty property, string label)
        {
            VisualElement root = new VisualElement();

            SerializedProperty propertyBase = property.FindPropertyRelative(PROP_BASE);
            SerializedProperty propertyBone = property.FindPropertyRelative(PROP_BONE);
            
            var fieldBase = new PropertyField(propertyBase, label);
            VisualElement fieldBone = BoneDrawer.CreatePropertyGUI(propertyBone, " ");
            
            root.Add(fieldBase);
            root.Add(fieldBone);

            fieldBase.RegisterValueChangeCallback(changeEvent =>
            {
                fieldBone.style.display = changeEvent.changedProperty.boolValue
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
            });
            
            fieldBone.style.display = propertyBase.boolValue
                ? DisplayStyle.Flex
                : DisplayStyle.None;
            
            return root;
        }
    }
}