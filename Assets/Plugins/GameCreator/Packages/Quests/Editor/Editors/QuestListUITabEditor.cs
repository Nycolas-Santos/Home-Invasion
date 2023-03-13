using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomEditor(typeof(QuestListUITab))]
    public class QuestListUITabEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            SerializedProperty questListUI = this.serializedObject.FindProperty("m_QuestListUI");
            SerializedProperty filterBy = this.serializedObject.FindProperty("m_FilterBy");

            root.Add(new PropertyTool(questListUI));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(filterBy));
            
            SerializedProperty activeFilter = this.serializedObject.FindProperty("m_ActiveFilter");
            
            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(activeFilter));

            return root;
        }
    }
}