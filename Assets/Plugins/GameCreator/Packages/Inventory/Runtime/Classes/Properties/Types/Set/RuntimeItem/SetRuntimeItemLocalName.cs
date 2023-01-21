using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

namespace GameCreator.Runtime.Inventory
{
    [Title("Local Name Variable")]
    [Category("Local Name Variable")]
    
    [Description("Sets the Runtime Item value on a Local Name Variable")]
    [Image(typeof(IconNameVariable), ColorTheme.Type.Purple)]

    [Serializable] [HideLabelsInEditor]
    public class SetRuntimeItemLocalName : PropertyTypeSetRuntimeItem
    {
        [SerializeField]
        protected FieldSetLocalName m_Variable = new FieldSetLocalName(ValueRuntimeItem.TYPE_ID);

        public override void Set(RuntimeItem value, Args args) => this.m_Variable.Set(value);
        public override void Set(RuntimeItem value, GameObject gameObject) => this.m_Variable.Set(value);

        public override RuntimeItem Get(Args args) => this.m_Variable.Get() as RuntimeItem;
        public override RuntimeItem Get(GameObject gameObject) => this.m_Variable.Get() as RuntimeItem;
        
        public static PropertySetRuntimeItem Create => new PropertySetRuntimeItem(
            new SetRuntimeItemLocalName()
        );
        
        public override string String => this.m_Variable.ToString();
    }
}