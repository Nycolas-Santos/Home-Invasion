using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    [CustomPropertyDrawer(typeof(Socket))]
    public class ItemSocketDrawer : PropertyDrawer
    {
        public const string PROP_BASE = "m_Base";
        public const string PROP_SOCKET_ID = "m_SocketID";
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty propertyBase = property.FindPropertyRelative(PROP_BASE);
            SerializedProperty propertySocketID = property.FindPropertyRelative(PROP_SOCKET_ID);

            var fieldBase = new GameCreator.Editor.Common.PropertyTool(propertyBase);
            var fieldSocketID = new GameCreator.Editor.Common.PropertyTool(propertySocketID);

            root.Add(fieldBase);
            root.Add(fieldSocketID);

            return root;
        }
    }
}