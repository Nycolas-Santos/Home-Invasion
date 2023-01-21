using System;
using UnityEngine;

namespace GameCreator.Runtime.Common
{
    [Serializable]
    public abstract class TEnablerValue<T> : TEnablerValueCommon
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private bool m_IsEnabled;
        [SerializeField] private T m_Value;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsEnabled
        {
            get => this.m_IsEnabled;
            set => this.m_IsEnabled = value;
        }

        public T Value
        {
            get => this.m_Value;
            set => this.m_Value = value;
        }

        // CONSTRUCTORS: --------------------------------------------------------------------------

        protected TEnablerValue()
        {
            this.m_IsEnabled = false;
            this.m_Value = default;
        }

        protected TEnablerValue(bool isEnabled, T value)
        {
            this.m_IsEnabled = isEnabled;
            this.m_Value = value;
        }
    }
}