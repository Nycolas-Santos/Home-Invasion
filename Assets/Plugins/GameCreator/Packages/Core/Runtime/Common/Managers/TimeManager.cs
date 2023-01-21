using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCreator.Runtime.Common
{
    [AddComponentMenu("")]
    public class TimeManager : Singleton<TimeManager>
    {
        private const int DEFAULT = 0;
        private const float PHYSICS_TIME_STEP = 0.02f;

        private const float ERROR = 0.01f;

        // STRUCTS: -------------------------------------------------------------------------------
        
        private readonly struct TimeData
        {
            private readonly float m_To;
            private readonly float m_From;

            private readonly float m_Transition;
            private readonly float m_StartTime;

            public TimeData(float transition, float to, float from = 1.0f)
            {
                this.m_To = to;
                this.m_From = from;

                this.m_Transition = transition;
                this.m_StartTime = Time.unscaledTime;
            }

            public float Get()
            {
                if (this.m_Transition <= ERROR) return this.m_To;

                float t = (Time.unscaledTime - this.m_StartTime) / this.m_Transition;
                return Mathf.Lerp(this.m_From, this.m_To, t);
            }
        }

        // MEMBERS: -------------------------------------------------------------------------------

        private readonly Dictionary<int, TimeData> m_TimeScales = new Dictionary<int, TimeData>();
        private float m_EndTime;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void SetTimeScale(float timeScale, int layer = DEFAULT)
        {
            this.m_TimeScales[layer] = new TimeData(0f, timeScale);
            this.RecalculateTimeScale();
        }

        public void SetSmoothTimeScale(float timeScale, float transition, int layer = DEFAULT)
        {
            if (transition < ERROR)
            {
                this.SetTimeScale(timeScale, layer);
                return;
            }
            
            this.m_EndTime = Mathf.Max(this.m_EndTime, Time.unscaledTime + transition);
            this.m_TimeScales[layer] = new TimeData(transition, timeScale, Time.timeScale);         
        }

        // UPDATE METHOD: -------------------------------------------------------------------------

        private void Update()
        {
            this.RecalculateTimeScale();
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void RecalculateTimeScale()
        {
            float scale = this.m_TimeScales.Count > 0 ? 99f : 1.0f;
            foreach (KeyValuePair<int, TimeData> item in this.m_TimeScales)
            {
                scale = Math.Min(scale, item.Value.Get());
            }

            Time.timeScale = scale;
            Time.fixedDeltaTime = PHYSICS_TIME_STEP * scale;
        }
    }
}