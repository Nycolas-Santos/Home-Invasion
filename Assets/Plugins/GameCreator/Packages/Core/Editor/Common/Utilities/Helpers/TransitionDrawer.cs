using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using GameCreator.Runtime.Common;

namespace GameCreator.Editor.Common
{
    [CustomPropertyDrawer(typeof(Transition))]
    public class TransitionDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();
            VisualElement head = new VisualElement();
            VisualElement body = new VisualElement();
            
            root.Add(head);
            root.Add(body);
            
            SerializedProperty duration = property.FindPropertyRelative("m_Duration");
            SerializedProperty easing = property.FindPropertyRelative("m_Easing");
            SerializedProperty time = property.FindPropertyRelative("m_Time");
            SerializedProperty wait = property.FindPropertyRelative("m_WaitToComplete");

            PropertyTool fieldDuration = new PropertyTool(duration);
            PropertyTool fieldEasing = new PropertyTool(easing);
            PropertyTool fieldTime = new PropertyTool(time);
            PropertyTool fieldWait = new PropertyTool(wait);

            head.Add(fieldDuration);
            
            fieldDuration.EventChange += _ =>
            {
                body.Clear();
                if (duration.floatValue <= float.Epsilon) return;
                
                body.Add(fieldEasing);
                body.Add(fieldTime);
                body.Add(fieldWait);
                    
                body.Bind(duration.serializedObject);
            };

            if (duration.floatValue > float.Epsilon)
            {
                body.Add(fieldEasing);
                body.Add(fieldTime);
                body.Add(fieldWait);
            }

            return root;
        }
    }
}