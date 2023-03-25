using System;
using UnityEngine;

namespace GameCreator.Runtime.Dialogue
{
    [Serializable]
    public class ValuesNodeChoices
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private bool m_HideUnavailable;
        [SerializeField] private bool m_HideVisited;
        [SerializeField] private bool m_SkipChoice;
        [SerializeField] private bool m_ShuffleChoices;

        [SerializeField] private TimedChoice m_TimedChoice = new TimedChoice();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool HideUnavailable => this.m_HideUnavailable;
        public bool HideVisited => this.m_HideVisited;
        public bool SkipChoice => this.m_SkipChoice;
        public bool ShuffleChoices => this.m_ShuffleChoices;
        
        public TimedChoice TimedChoice => this.m_TimedChoice;
    }
}