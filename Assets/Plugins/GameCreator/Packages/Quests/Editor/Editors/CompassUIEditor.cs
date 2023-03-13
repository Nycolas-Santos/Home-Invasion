using GameCreator.Editor.Common;
using GameCreator.Runtime.Quests.UnityUI;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Quests
{
    [CustomEditor(typeof(CompassUI))]
    public class CompassUIEditor : UnityEditor.Editor
    {
        // PAINT METHODS: -------------------------------------------------------------------------
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            SerializedProperty character = this.serializedObject.FindProperty("m_Character");
            SerializedProperty camera = this.serializedObject.FindProperty("m_Camera");
            SerializedProperty fov = this.serializedObject.FindProperty("m_FieldOfView");
            SerializedProperty layers = this.serializedObject.FindProperty("m_Layers");
            
            root.Add(new PropertyTool(character));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(camera));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(fov));
            root.Add(new SpaceSmaller());
            root.Add(new PropertyTool(layers));

            SerializedProperty prefab = this.serializedObject.FindProperty("m_Prefab");
            SerializedProperty content = this.serializedObject.FindProperty("m_Content");

            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(prefab));
            root.Add(new PropertyTool(content)); 
            
            SerializedProperty showHidden = this.serializedObject.FindProperty("m_HiddenQuests");
            SerializedProperty hideUntrack = this.serializedObject.FindProperty("m_HideUntracked");

            root.Add(new SpaceSmall());
            root.Add(new PropertyTool(showHidden));
            root.Add(new PropertyTool(hideUntrack));

            return root;
        }
    }
}