using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace GameCreator.Runtime.Inventory
{
    [Title("Local Name Variable")]
    [Category("Local Name Variable")]
    
    [Description("Sets the Item value on a Local Name Variable")]
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple)]

    [Serializable] [HideLabelsInEditor]
    public class SetItemLocalName : PropertyTypeSetItem
    {
        [SerializeField]
        protected FieldSetLocalName m_Variable = new FieldSetLocalName(ValueItem.TYPE_ID);

        public override void Set(Item value, Args args) => this.m_Variable.Set(value);
        public override void Set(Item value, GameObject gameObject) => this.m_Variable.Set(value);

        public override Item Get(Args args) => this.m_Variable.Get() as Item;
        public override Item Get(GameObject gameObject) => this.m_Variable.Get() as Item;
        
        public static PropertySetItem Create => new PropertySetItem(
            new SetItemLocalName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}