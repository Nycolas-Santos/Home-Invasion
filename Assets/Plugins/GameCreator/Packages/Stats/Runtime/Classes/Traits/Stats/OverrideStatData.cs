using System;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public class OverrideStatData
    {
        #pragma warning disable 414
        [SerializeField] [HideInInspector] 
        private bool m_IsExpanded = false;
        #pragma warning restore 414
        
        [SerializeField] private bool m_ChangeBase = false;
        [SerializeField] private double m_Base = 1;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool ChangeBase => m_ChangeBase;
        public double Base => m_Base;
    }
}