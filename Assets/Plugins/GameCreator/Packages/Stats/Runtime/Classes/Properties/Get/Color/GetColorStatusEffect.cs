using System;
using UnityEngine;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.Stats
{
    [Title("Status Effect Color")]
    [Category("Stats/Status Effect Color")]
    
    [Image(typeof(IconStatusEffect), ColorTheme.Type.Green)]
    [Description("Returns the Color value of a Status Effect")]

    [Serializable] [HideLabelsInEditor]
    public class GetColorStatusEffect : PropertyTypeGetColor
    {
        [SerializeField] private StatusEffect m_StatusEffect;

        public override Color Get(Args args) => this.m_StatusEffect != null 
            ? this.m_StatusEffect.Color 
            : Color.black;

        public override string String => this.m_StatusEffect != null
            ? this.m_StatusEffect.ID.String 
            : "(none)";
    }
}