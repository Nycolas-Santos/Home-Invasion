using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace GameCreator.Runtime.Inventory
{
    [Title("Local List Variable")]
    [Category("Local List Variable")]
    
    [Description("Sets the Item value on a Local List Variable")]
    [Image(typeof(IconListVariable), ColorTheme.Type.Teal)]

    [Serializable] [HideLabelsInEditor]
    public class SetItemLocalList : PropertyTypeSetItem
    {
        [SerializeField]
        protected FieldSetLocalList m_Variable = new FieldSetLocalList(ValueItem.TYPE_ID);

        public override void Set(Item value, Args args) => this.m_Variable.Set(value);
        public override void Set(Item value, GameObject gameObject) => this.m_Variable.Set(value);

        public override Item Get(Args args) => this.m_Variable.Get() as Item;
        public override Item Get(GameObject gameObject) => this.m_Variable.Get() as Item;
        
        public static PropertySetItem Create => new PropertySetItem(
            new SetItemLocalList()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}