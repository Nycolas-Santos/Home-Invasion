using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Dialogue
{
    [Serializable]
    public class Visits
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private VisitsNodes m_Nodes = new VisitsNodes();
        [SerializeField] private VisitsTags m_Tags = new VisitsTags();
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private bool m_IsVisited;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public VisitsNodes Nodes => this.m_Nodes;
        
        public VisitsTags Tags => this.m_Tags;
        
        public bool IsVisited
        {
            get => this.m_IsVisited;
            set => this.m_IsVisited = value;
        }
    }
}