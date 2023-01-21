using GameCreator.Runtime.Inventory;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Inventory
{
    public abstract class TBagShapeWithWeightDrawer : TBagShapeDrawer
    {
        // EDITOR: --------------------------------------------------------------------------------
        
        protected override void EditorContent(SerializedProperty property, VisualElement root)
        {
            SerializedProperty hasMaxWeight = property.FindPropertyRelative("m_HasMaxWeight");
            SerializedProperty maxWeight = property.FindPropertyRelative("m_MaxWeight");

            var fieldHasMaxWeight = new GameCreator.Editor.Common.PropertyTool(hasMaxWeight);
            var fieldMaxWeight = new GameCreator.Editor.Common.PropertyTool(maxWeight);
            VisualElement weightContentMaxWeight = new VisualElement();
            
            root.Add(fieldHasMaxWeight);
            root.Add(weightContentMaxWeight);
            
            fieldHasMaxWeight.EventChange += changeEvent =>
            {
                weightContentMaxWeight.Clear();
                if (changeEvent.changedProperty.boolValue)
                {
                    weightContentMaxWeight.Add(fieldMaxWeight);
                    weightContentMaxWeight.Bind(changeEvent.changedProperty.serializedObject);
                }
            };
            
            if (hasMaxWeight.boolValue) weightContentMaxWeight.Add(fieldMaxWeight);
        }
        
        // RUNTIME: -------------------------------------------------------------------------------

        protected override void RuntimeContent(IBagShape shape, VisualElement root)
        {
            if (shape is TBagShapeWithWeight { HasMaxWeight: true } shapeWithWeight)
            {
                TextField fieldWeight = new TextField("Max Weight")
                {
                    value = shapeWithWeight.MaxWeight.ToString()
                };
                
                fieldWeight.SetEnabled(false);
                root.Add(fieldWeight);
            }
        }
    }
}