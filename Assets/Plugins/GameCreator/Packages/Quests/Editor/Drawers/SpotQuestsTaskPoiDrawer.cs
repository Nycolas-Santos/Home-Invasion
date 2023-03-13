using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomPropertyDrawer(typeof(SpotQuestsTaskPoi))]
    public class SpotQuestsTaskPoiDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty journal = property.FindPropertyRelative("m_Journal");
            SerializedProperty task = property.FindPropertyRelative("m_Task");

            root.Add(new PropertyTool(journal));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(task));

            SerializedProperty offset = property.FindPropertyRelative("m_Offset");
            SerializedProperty space = property.FindPropertyRelative("m_Space");
            SerializedProperty layers = property.FindPropertyRelative("m_Layers");

            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(offset));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(space));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(layers));

            return root;
        }
    }
}