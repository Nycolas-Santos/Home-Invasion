using System;
using UnityEngine;

namespace GameCreator.Runtime.Characters
{
    public class Target
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private GameObject m_Target;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public GameObject On
        {
            get => this.m_Target;
            set
            {
                this.m_Target = value;
                this.EventChange?.Invoke(this.m_Target);
            }
        }
        
        // EVENTS: --------------------------------------------------------------------------------

        public event Action<GameObject> EventChange;
    }
}