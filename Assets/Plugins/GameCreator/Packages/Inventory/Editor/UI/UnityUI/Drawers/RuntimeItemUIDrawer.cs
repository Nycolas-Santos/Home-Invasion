using GameCreator.Editor.Common;
using GameCreator.Runtime.Inventory.UnityUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory.UnityUI
{
    [CustomPropertyDrawer(typeof(RuntimeItemUI))]
    public class RuntimeItemUIDrawer : TItemUIDrawer
    {
        protected override void AddBefore(VisualElement root, SerializedProperty property)
        {
            base.AddBefore(root, property);
            SerializedProperty activeContent = property.FindPropertyRelative("m_ActiveContent");
            SerializedProperty activeEquipped = property.FindPropertyRelative("m_ActiveEquipped");
            SerializedProperty activeNotEquipped = property.FindPropertyRelative("m_ActiveNotEquipped");
            
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeContent));
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeEquipped));
            root.Add(new GameCreator.Editor.Common.PropertyTool(activeNotEquipped));
            root.Add(new SpaceSmall());
        }
    }
}
