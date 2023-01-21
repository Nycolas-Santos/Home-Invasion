using GameCreator.Editor.Common;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    public abstract class TWeaponEditor : UnityEditor.Editor
    {
        protected const string CLASS_MARGIN_X = "gc-inspector-margins-x";
        protected const string CLASS_MARGIN_Y = "gc-inspector-margins-y";
        
        public sealed override bool UseDefaultMargins() => false;

        public sealed override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            StyleSheet[] sheets = StyleSheetUtils.Load();
            foreach (StyleSheet sheet in sheets) root.styleSheets.Add(sheet);
            
            VisualElement head = new VisualElement();
            VisualElement body = new VisualElement();
            VisualElement foot = new VisualElement();
            
            head.AddToClassList(CLASS_MARGIN_X);
            head.AddToClassList(CLASS_MARGIN_Y);
            
            foot.AddToClassList(CLASS_MARGIN_X);
            foot.AddToClassList(CLASS_MARGIN_Y);
            
            SerializedProperty title = this.serializedObject.FindProperty("m_Title");
            SerializedProperty description = this.serializedObject.FindProperty("m_Description");
            SerializedProperty icon = this.serializedObject.FindProperty("m_Icon");
            SerializedProperty color = this.serializedObject.FindProperty("m_Color");
            
            head.Add(new PropertyTool(title));
            head.Add(new SpaceSmaller());
            head.Add(new PropertyTool(description));
            head.Add(new SpaceSmaller());
            head.Add(new PropertyTool(icon));
            head.Add(new SpaceSmaller());
            head.Add(new PropertyTool(color));
            
            SerializedProperty reaction = this.serializedObject.FindProperty("m_Reaction");
            
            head.Add(new SpaceSmaller());
            head.Add(new PropertyTool(reaction));

            SerializedProperty id = this.serializedObject.FindProperty("m_Id");
            head.Add(new SpaceSmall());
            head.Add(new PropertyField(id));

            this.CreateGUI(body);

            SerializedProperty onEquip = this.serializedObject.FindProperty("m_OnEquip");
            SerializedProperty onUnequip = this.serializedObject.FindProperty("m_OnUnequip");

            foot.Add(new SpaceSmall());
            foot.Add(new LabelTitle("On Equip:"));
            foot.Add(new SpaceSmaller());
            foot.Add(new PropertyField(onEquip));
            
            foot.Add(new SpaceSmall());
            foot.Add(new LabelTitle("On Unequip:"));
            foot.Add(new SpaceSmaller());
            foot.Add(new PropertyField(onUnequip));
            
            root.Add(head);
            root.Add(body);
            root.Add(foot);
            
            return root;
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected abstract void CreateGUI(VisualElement root);
    }
}