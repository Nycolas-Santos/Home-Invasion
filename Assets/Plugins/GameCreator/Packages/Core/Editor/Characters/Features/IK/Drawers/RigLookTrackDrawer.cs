using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters.IK;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    [CustomPropertyDrawer(typeof(RigLookTrack))]
    public class RigLookTrackDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty trackSpeed = property.FindPropertyRelative("m_TrackSpeed");
            SerializedProperty maxAngle = property.FindPropertyRelative("m_MaxAngle");
            
            SerializedProperty headWeight = property.FindPropertyRelative("m_HeadWeight");
            SerializedProperty neckWeight = property.FindPropertyRelative("m_NeckWeight");
            SerializedProperty chestWeight = property.FindPropertyRelative("m_ChestWeight");
            SerializedProperty spineWeight = property.FindPropertyRelative("m_SpineWeight");
            
            VisualElement root = new VisualElement();
            
            root.Add(new PropertyTool(trackSpeed));
            root.Add(new PropertyTool(maxAngle));
            
            root.Add(new PropertyTool(headWeight));
            root.Add(new PropertyTool(neckWeight));
            root.Add(new PropertyTool(chestWeight));
            root.Add(new PropertyTool(spineWeight));

            return root;
        }
    }
}