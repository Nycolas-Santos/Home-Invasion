using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Stats
{
    internal class RuntimeStatusEffectData : ICancellable
    {
        // MEMBERS: -------------------------------------------------------------------------------
        
        private readonly float m_StartTime;
        private readonly float m_Duration;

        private readonly StatusEffect m_StatusEffect;

        [System.NonSerialized] private GameObject m_OnStart;
        [System.NonSerialized] private GameObject m_OnEnd;
        [System.NonSerialized] private GameObject m_WhileActive;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        private Args Args { get; }

        public bool IsCancelled { get; private set; }

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public RuntimeStatusEffectData(Traits traits, StatusEffect statusEffect, float elapsedTime = 0f)
        {
            this.Args = new Args(traits.gameObject);
            this.m_StartTime = Time.time - elapsedTime;
            this.m_Duration = statusEffect.GetDuration(this.Args);

            this.m_StatusEffect = statusEffect;

            _ = this.RunOnStart();
            _ = this.RunWhileActive();
        }

        // INTERNAL METHODS: ----------------------------------------------------------------------

        public bool Update()
        {
            if (!this.m_StatusEffect.HasDuration) return false;
            return Time.time > this.m_StartTime + this.m_Duration;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private async Task RunOnStart()
        {
            StatusEffect.LastAdded = this.m_StatusEffect;
            
            if (this.m_OnStart == null)
            {
                this.m_OnStart = RunInstructionsList.CreateTemplate(
                    this.m_StatusEffect.OnStart.List
                );   
            }
            
            await RunInstructionsList.Run(
                this.Args.Clone, this.m_OnStart,
                new RunnerConfig
                {
                    Name = $"On Start {TextUtils.Humanize(this.m_StatusEffect.name)}",
                    Location = new RunnerLocationPosition(
                        this.Args.Self != null ? this.Args.Self.transform.position : Vector3.zero, 
                        this.Args.Self != null ? this.Args.Self.transform.rotation : Quaternion.identity
                    )
                }
            );
        }
        
        private async Task RunOnEnd()
        {
            StatusEffect.LastRemoved = this.m_StatusEffect;
            
            if (this.m_OnEnd == null)
            {
                this.m_OnEnd = RunInstructionsList.CreateTemplate(
                    this.m_StatusEffect.OnEnd.List
                );   
            }
            
            await RunInstructionsList.Run(
                this.Args.Clone, this.m_OnEnd,
                new RunnerConfig
                {
                    Name = $"On End {TextUtils.Humanize(this.m_StatusEffect.name)}",
                    Location = new RunnerLocationPosition(
                        this.Args.Self != null ? this.Args.Self.transform.position : Vector3.zero, 
                        this.Args.Self != null ? this.Args.Self.transform.rotation : Quaternion.identity
                    )
                }
            );
        }

        private async Task RunWhileActive()
        {
            if (this.m_WhileActive == null)
            {
                this.m_WhileActive = RunInstructionsList.CreateTemplate(
                    this.m_StatusEffect.OnWhileActive.List
                );   
            }

            while (this.m_WhileActive != null)
            {
                int frame = Time.frameCount;
                
                await RunInstructionsList.Run(
                    this.Args.Clone, this.m_WhileActive,
                    new RunnerConfig
                    {
                        Name = $"While Active {TextUtils.Humanize(this.m_StatusEffect.name)}",
                        Location = new RunnerLocationPosition(
                            this.Args.Self != null ? this.Args.Self.transform.position : Vector3.zero, 
                            this.Args.Self != null ? this.Args.Self.transform.rotation : Quaternion.identity
                        ),
                        Cancellable = this
                    }
                );

                if (frame == Time.frameCount) await Task.Yield();
            }
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Stop()
        {
            if (this.m_WhileActive != null) this.IsCancelled = true;
            _ = this.RunOnEnd();
        }

        public RuntimeStatusEffectValue GetValue()
        {
            float timeElapsed = Time.time - this.m_StartTime;

            if (this.m_StatusEffect.HasDuration)
            {
                return new RuntimeStatusEffectValue(
                    this.m_StatusEffect.ID.String,
                    timeElapsed, this.m_StatusEffect.GetDuration(this.Args) - timeElapsed
                );
            }
            
            return new RuntimeStatusEffectValue(
                this.m_StatusEffect.ID.String,
                timeElapsed, -1f
            );
        }
    }
}