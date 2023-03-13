using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomPropertyDrawer(typeof(SpotQuestsCustomPoi))]
    public class SpotQuestsCustomPoiDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty name = property.FindPropertyRelative("m_Name");
            SerializedProperty sprite = property.FindPropertyRelative("m_Sprite");
            SerializedProperty color = property.FindPropertyRelative("m_Color");

            root.Add(new PropertyTool(name));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(sprite));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(color));
            
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