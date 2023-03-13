using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [Image(typeof(IconStat), ColorTheme.Type.Red)]
    
    [Serializable]
    public class StatItem : TPolymorphicItem<StatItem>
    {
        [SerializeField] private bool m_IsHidden = false;
        [SerializeField] private Stat m_Stat;

        [SerializeField] private bool m_ChangeBase = false;
        [SerializeField] private double m_Base = 100;
        
        [SerializeField] private bool m_ChangeFormula = false;
        [SerializeField] private Formula m_Formula;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsHidden => m_IsHidden;
        public Stat Stat => this.m_Stat;

        public override string Title => this.m_Stat != null 
            ? TextUtils.Humanize(this.m_Stat.ID.String) 
            : "(none)";

        public double Base
        {
            get
            {
                if (this.m_Stat == null) return 0f;
                return this.m_ChangeBase ? this.m_Base : this.m_Stat.Value;
            }
        }

        public Formula Formula
        {
            get
            {
                if (this.m_Stat == null) return null;
                return this.m_ChangeFormula ? this.m_Formula : this.m_Stat.Formula;
            }
        }
    }
}