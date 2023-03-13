using System;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public class OverrideAttributeData
    {
        #pragma warning disable 414
        [SerializeField] [HideInInspector] 
        private bool m_IsExpanded = false;
        #pragma warning restore 414
        
        [SerializeField] private bool m_ChangeStartPercent = false;
        [SerializeField] [Range(0f, 1f)] private double m_StartPercent = 1;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool ChangeStartPercent => m_ChangeStartPercent;
        public double StartPercent => m_StartPercent;
    }
}