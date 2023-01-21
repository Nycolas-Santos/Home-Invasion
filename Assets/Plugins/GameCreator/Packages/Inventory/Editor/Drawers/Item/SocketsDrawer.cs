using GameCreator.Editor.Common;
using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(Sockets))]
    public class SocketsDrawer : TBoxDrawer
    {
        private const string PROP_LIST = "m_List";
        
        protected override string Name(SerializedProperty property) => "Sockets";

        protected override void CreatePropertyContent(VisualElement container, SerializedProperty property)
        {
            SerializedProperty prefabSocket = property.FindPropertyRelative("m_PrefabSocket");
            SerializedProperty inheritFromParent = property.FindPropertyRelative("m_InheritFromParent");

            var fieldPrefabSocket = new GameCreator.Editor.Common.PropertyTool(prefabSocket);
            var fieldInheritFromParent = new GameCreator.Editor.Common.PropertyTool(inheritFromParent);
            SocketsTool fieldSockets = new SocketsTool(property, PROP_LIST);

            container.Add(fieldPrefabSocket);
            container.Add(fieldInheritFromParent);
            container.Add(fieldSockets);
        }
    }
}