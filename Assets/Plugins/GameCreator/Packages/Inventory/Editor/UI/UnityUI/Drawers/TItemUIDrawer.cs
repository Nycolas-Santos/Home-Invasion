using GameCreator.Editor.Common;
using GameCreator.Runtime.Inventory.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory.UnityUI
{
    [CustomPropertyDrawer(typeof(TItemUI))]
    public class TItemUIDrawer : TBoxDrawer
    {
        protected sealed override void CreatePropertyContent(VisualElement root, SerializedProperty property)
        {
            SerializedProperty name = property.FindPropertyRelative("m_Name");
            SerializedProperty description = property.FindPropertyRelative("m_Description");
            SerializedProperty image = property.FindPropertyRelative("m_Icon");
            SerializedProperty color = property.FindPropertyRelative("m_Color");
            
            SerializedProperty activeCanUse = property.FindPropertyRelative("m_ActiveIsUsable");
            SerializedProperty activeCanCraft = property.FindPropertyRelative("m_ActiveIsCraftable");
            SerializedProperty activeCanDismantle = property.FindPropertyRelative("m_ActiveIsDismantable");
            SerializedProperty activeCanDrop = property.FindPropertyRelative("m_ActiveIsDroppable");
            SerializedProperty activeCanEquip = property.FindPropertyRelative("m_ActiveIsEquippable");

            this.AddBefore(root, property);
            
            root.Add(new GameCreator.Editor.Common.PropertyTool(name));
            root.Add(new GameCreator.Editor.Common.PropertyTool(description));
            root.Add(new SpaceSmall());
            root.Add(new GameCreator.Editor.Common.PropertyTool(image));
            root.Add(new GameCreator.Editor.Common.PropertyTool(color));
            root.Add(new SpaceSmall());
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeCanUse));
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeCanCraft));
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeCanDismantle));
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeCanDrop));
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeCanEquip));
            
            SerializedProperty width = property.FindPropertyRelative("m_Width");
            SerializedProperty height = property.FindPropertyRelative("m_Height");
            SerializedProperty weight = property.FindPropertyRelative("m_Weight");
            SerializedProperty price = property.FindPropertyRelative("m_Price");
            SerializedProperty activeHasProperties = property.FindPropertyRelative("m_ActiveHasProperties");
            SerializedProperty prefabProperty = property.FindPropertyRelative("m_PrefabProperty");
            SerializedProperty propertiesContent = property.FindPropertyRelative("m_PropertiesContent");
            SerializedProperty activeHasSockets = property.FindPropertyRelative("m_ActiveHasSockets");
            SerializedProperty socketsCount = property.FindPropertyRelative("m_SocketsCount");
            SerializedProperty prefabSocket = property.FindPropertyRelative("m_PrefabSocket");
            SerializedProperty socketsContent = property.FindPropertyRelative("m_SocketsContent");
            
            root.Add(new SpaceSmall());
            root.Add(new GameCreator.Editor.Common.PropertyTool(weight));
            root.Add(new GameCreator.Editor.Common.PropertyTool(width));
            root.Add(new GameCreator.Editor.Common.PropertyTool(height));
            root.Add(new GameCreator.Editor.Common.PropertyTool(price));
            root.Add(new SpaceSmall());
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeHasProperties));
            root.Add(new GameCreator.Editor.Common.PropertyTool(prefabProperty));
            root.Add(new GameCreator.Editor.Common.PropertyTool(propertiesContent));
            root.Add(new SpaceSmall());
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeHasSockets));
            root.Add(new GameCreator.Editor.Common.PropertyTool(socketsCount));
            root.Add(new GameCreator.Editor.Common.PropertyTool(prefabSocket));
            root.Add(new GameCreator.Editor.Common.PropertyTool(socketsContent));
            
            this.AddAfter(root, property);
        }

        protected virtual void AddBefore(VisualElement root, SerializedProperty property)
        { }
        
        protected virtual void AddAfter(VisualElement root, SerializedProperty property)
        { }
    }
}
