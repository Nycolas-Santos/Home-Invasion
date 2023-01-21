using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(ReactionItem))]
    public class ReactionItemDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            SerializedProperty minPower = property.FindPropertyRelative("m_MinPower");
            SerializedProperty direction = property.FindPropertyRelative("m_Direction");
            
            root.Add(new PropertyTool(minPower));
            root.Add(new SpaceSmallest());
            root.Add(new PropertyTool(direction));
            
            SerializedProperty conditions = property.FindPropertyRelative("m_Conditions");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyField(conditions));
            
            SerializedProperty animations = property.FindPropertyRelative("m_Animations");
            SerializedProperty avatarMask = property.FindPropertyRelative("m_AvatarMask");

            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(avatarMask));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyField(animations));

            return root;
        }
    }
}