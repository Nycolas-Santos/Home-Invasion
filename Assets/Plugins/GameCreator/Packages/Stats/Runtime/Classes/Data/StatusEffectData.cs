using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    [Serializable]
    public class StatusEffectData
    {
        [SerializeField] private StatusEffectType m_Type = StatusEffectType.Positive;
        [SerializeField] private PropertyGetInteger m_MaxStack = new PropertyGetInteger(1);

        [SerializeField] private bool m_Save = true;
        [SerializeField] private bool m_HasDuration = false;
        [SerializeField] private PropertyGetDecimal m_Duration = new PropertyGetDecimal(60f);

        // PROPERTIES: ----------------------------------------------------------------------------

        public StatusEffectType Type => this.m_Type;

        public bool Save => this.m_Save;
        
        public bool HasDuration => this.m_HasDuration;
        
        // METHODS: -------------------------------------------------------------------------------

        public double GetDuration(Args args)
        {
            return this.HasDuration 
                ? this.m_Duration.Get(args) 
                : -1;
        }

        public int GetMaxStack(Args args)
        {
            float maxStack = (float) this.m_MaxStack.Get(args);
            return Mathf.FloorToInt(maxStack);
        }
    }
}