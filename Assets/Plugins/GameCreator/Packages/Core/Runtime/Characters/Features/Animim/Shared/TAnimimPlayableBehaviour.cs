using System;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GameCreator.Runtime.Characters.Animim
{
    public abstract class TAnimimPlayableBehaviour : PlayableBehaviour
    {
        private const string RTC_PATH = "GameCreator/AnimationClip";
        private static RuntimeAnimatorController RTC_ANIMATION;

        // FIELDS: --------------------------------------------------------------------------------
        
        public AnimationLayerMixerPlayable mixerPlayable;
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] protected Playable m_Playable;
        
        [NonSerialized] protected readonly AvatarMask m_AvatarMask;
        [NonSerialized] protected readonly BlendMode m_BlendMode;
        
        [NonSerialized] protected readonly AnimimGraph m_AnimimGraph;
        [NonSerialized] protected readonly IConfig m_Config;

        [NonSerialized] private TAnimimOutput m_ParentOutput;

        // PROPERTIES: ----------------------------------------------------------------------------
        
        [field: NonSerialized] protected AnimatorControllerPlayable Playable { get; set; }

        [field: NonSerialized] protected AnimFloat Weight { get; }
        [field: NonSerialized] public double Speed { get; set; }

        [field: NonSerialized] protected double StartTime { get; }
        [field: NonSerialized] protected double CurrentTime { get; private set; }

        [field: NonSerialized] public bool IsComplete { get; private set; }

        public float RootMotion => this.m_Config?.RootMotion ?? false 
            ? this.Weight.Current 
            : 0f;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        protected TAnimimPlayableBehaviour(AvatarMask avatarMask, BlendMode blendMode, 
            AnimimGraph animimGraph, IConfig config)
        {
            this.m_AvatarMask = avatarMask;
            this.m_BlendMode = blendMode;
            
            this.m_AnimimGraph = animimGraph;
            this.m_Config = config;

            this.StartTime = animimGraph.Character.Time.TimeAsDouble;
            
            this.Weight = new AnimFloat(0f, this.m_Config.TransitionIn);
            this.Speed = this.m_Config.Speed;
        }

        // OVERRIDE METHODS: ----------------------------------------------------------------------
        
        public override void OnPlayableCreate(Playable playable)
        {
            base.OnPlayableCreate(playable);
            this.m_Playable = playable;
            
            if (this.m_Config.Duration > float.Epsilon)
            {
                float totalDuration = this.m_Config.DelayIn + this.m_Config.Duration;
                playable.SetDuration(totalDuration);
            }
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            base.PrepareFrame(playable, info);
            
            playable.SetSpeed(this.Speed);

            this.CurrentTime = playable.GetTime();
            this.UpdateFrame();

            playable.GetInput(0).SetInputWeight(1, this.Weight.Current);

            if (playable.IsDone())
            {
                Playable mixer = playable.GetInput(0);
                Playable source = mixer.GetInput(0);
                Playable parent = playable.GetOutput(0);
                
                mixer.DisconnectInput(0);
                parent.DisconnectInput(0);
                
                parent.ConnectInput(0, source, 0);
                parent.SetInputWeight(0, 1f);
                
                playable.Destroy();
                this.m_ParentOutput.OnDeleteChild(this);
                this.IsComplete = true;
            }
            
            if (!this.Playable.IsValid()) return;
            if (this.Playable.IsDone()) return;

            for (int i = 0; i < Phases.Count; ++i)
            {
                float value = this.Playable.GetFloat(Phases.HASH_PHASES[i]);
                this.m_AnimimGraph.Phases.Set(i, value, this.Weight.Current);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void UpdateFrame()
        {
            double elapsedTime = this.m_AnimimGraph.Character.Time.TimeAsDouble - this.StartTime;

            if (elapsedTime < this.m_Config.DelayIn)
            {
                this.Playable.Pause();
            }
            else
            {
                this.Playable.Play();
                
                this.Weight.Target = this.m_Config.Weight;
                this.Weight.Smooth = this.m_Config.TransitionIn;
            }

            if (this.m_Config.Duration > float.Epsilon)
            {
                float timeToFadeout = Math.Max(
                    this.m_Config.Duration - this.m_Config.TransitionOut, 
                    this.m_Config.TransitionIn
                );
                
                if (elapsedTime >= this.m_Config.DelayIn + timeToFadeout)
                {
                    this.Weight.Target = 0f;
                    this.Weight.Smooth = this.m_Config.TransitionOut;
                }
            }
            
            this.Weight.Update();
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public void Create(TAnimimOutput parentOutput)
        {
            this.m_ParentOutput = parentOutput;
            
            Playable source = this.m_Playable.GetInput(0);
            this.m_Playable.DisconnectInput(0);
            
            this.mixerPlayable = AnimationLayerMixerPlayable.Create(
                this.m_AnimimGraph.Graph, 2
            );
            
            this.mixerPlayable.ConnectInput(0, source, 0);
            this.mixerPlayable.ConnectInput(1, this.Playable, 0);

            this.m_Playable.ConnectInput(0, this.mixerPlayable, 0);
            this.m_Playable.SetInputWeight(0, 1f);

            if (this.m_AvatarMask != null)
            {
                this.mixerPlayable.SetLayerMaskFromAvatarMask(1, this.m_AvatarMask);
            }
            
            this.mixerPlayable.SetLayerAdditive(1, this.m_BlendMode == BlendMode.Additive);
            
            this.mixerPlayable.SetInputWeight(0, 1f);
            this.mixerPlayable.SetInputWeight(1, 0f);
        }

        public void Stop()
        {
            this.Stop(0f, this.m_Config.TransitionOut);
        }

        public virtual void Stop(float delay, float transitionOut)
        {
            double duration = this.CurrentTime + delay + transitionOut;
            this.m_Playable.SetDuration(duration);

            this.m_Config.DelayIn = 0f;
            this.m_Config.Duration = (float) duration;
            this.m_Config.TransitionOut = transitionOut;
        }

        public void ChangeWeight(float weight, float transition)
        {
            this.m_Config.Weight = weight;
            this.m_Config.TransitionIn = transition;
        }

        // PROTECTED STATIC METHODS: --------------------------------------------------------------
        
        protected static AnimatorOverrideController CreateController(AnimationClip animationClip)
        {
            if (RTC_ANIMATION == null)
            {
                RTC_ANIMATION = Resources.Load<RuntimeAnimatorController>(RTC_PATH);
            }

            AnimatorOverrideController controller = new AnimatorOverrideController(RTC_ANIMATION);
            
            string key = RTC_ANIMATION.animationClips[0].name;
            controller[key] = animationClip;

            return controller;
        }
    }
}
