using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Common
{
    [CustomPropertyDrawer(typeof(NavLinkType))]
    public class NavLinkTypeDrawer : TNavAreaDrawer
    {
        private const string EMPTY_LABEL = " ";
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty linkType = property.FindPropertyRelative("m_LinkType");
            SerializedProperty forAreas = property.FindPropertyRelative("m_ForAreas");

            PropertyTool fieldLinkType = new PropertyTool(linkType);
            PropertyTool fieldForAreas = new PropertyTool(forAreas, EMPTY_LABEL);
            
            fieldForAreas.style.display = linkType.intValue == (int) NavLinkType.LinkType.Manual
                ? DisplayStyle.Flex
                : DisplayStyle.None;
            
            fieldLinkType.EventChange += changeEvent =>
            {
                fieldForAreas.style.display = linkType.intValue == (int) NavLinkType.LinkType.Manual
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;

                SerializedObject serializedObject = changeEvent.changedProperty.serializedObject;
                SerializationUtils.ApplyUnregisteredSerialization(serializedObject);
            };

            root.Add(fieldLinkType);
            root.Add(fieldForAreas);
            
            return root;
        }
    }
}