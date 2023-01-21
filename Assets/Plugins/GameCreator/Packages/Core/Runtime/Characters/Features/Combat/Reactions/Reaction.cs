using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Characters
{
    public abstract class Reaction : ScriptableObject, IReaction
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private float m_TransitionIn = 0.1f;
        [SerializeField] private float m_TransitionOut = 0.25f;

        [SerializeField] private bool m_UseRootMotion = true;
        [SerializeField] private PropertyGetDecimal m_Speed = GetDecimalConstantOne.Create;

        [SerializeField] private ReactionList m_ReactionList = new ReactionList();
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public float TransitionIn => this.m_TransitionIn;
        public float TransitionOut => this.m_TransitionOut;
        
        public bool UseRootMotion => this.m_UseRootMotion;

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool CanRun(Character character, Args args, ReactionInput input)
        {
            if (character == null) return false;

            ReactionItem reaction = this.m_ReactionList.Get(args, input.Direction, input.Power);
            return reaction != null;
        }

        public virtual ReactionOutput Run(Character character, Args args, ReactionInput input)
        {
            if (character == null) return new ReactionOutput();

            ReactionItem reaction = this.m_ReactionList.Get(args, input.Direction, input.Power);
            if (reaction == null) return new ReactionOutput();

            AnimationClip animationClip = reaction.AnimationClip;
            AvatarMask avatarMask = reaction.AvatarMask;
            float speed = (float) this.m_Speed.Get(args);
            
            if (animationClip == null) return new ReactionOutput();

            ReactionOutput output = new ReactionOutput(animationClip.length, speed, this);
            
            ConfigGesture config = new ConfigGesture(
                0f, animationClip != null ? animationClip.length : 0f, speed,
                this.m_UseRootMotion, 
                this.m_TransitionIn,
                this.m_TransitionOut
            );
            
            _ =  character.Gestures.CrossFade(
                animationClip, avatarMask, 
                BlendMode.Blend, 
                config, 
                true
            );

            return output;
        }
    }
}