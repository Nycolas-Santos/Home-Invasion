using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Stats
{
    [Title("Status Effect Name")]
    [Category("Stats/Status Effect Name")]

    [Image(typeof(IconStatusEffect), ColorTheme.Type.Green)]
    [Description("Returns the name of a Status Effect")]
    
    [Serializable] [HideLabelsInEditor]
    public class GetStringStatusEffectName : PropertyTypeGetString
    {
        [SerializeField] protected StatusEffect m_StatusEffect;

        public override string Get(Args args) => this.m_StatusEffect != null 
            ? this.m_StatusEffect.GetName(args) 
            : string.Empty;

        public override string String => this.m_StatusEffect != null 
            ? this.m_StatusEffect.ID.String 
            : "(none)";
    }
}