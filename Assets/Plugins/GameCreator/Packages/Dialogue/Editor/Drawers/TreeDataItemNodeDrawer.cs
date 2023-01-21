using GameCreator.Editor.Common;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Dialogue;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Dialogue
{
    [CustomPropertyDrawer(typeof(TTreeDataItem<Node>))]
    public class TreeDataItemNodeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty node = property.FindPropertyRelative(TTreeDataItem<Node>.NAME_VALUE);
            return new PropertyTool(node);
        }
    }
}