using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(0, 1, 1)]

    [Title("Dash")]
    [Description("Moves the Character in the chosen direction for a brief period of time")]

    [Category("Characters/Navigation/Dash")]

    [Parameter("Direction", "Vector oriented towards the desired direction")]
    [Parameter("Speed", "Velocity the Character moves throughout the whole movement")]
    [Parameter("Damping", "Defines the duration and gradually changes the rate of the movement over time")]
    [Parameter("Wait to Finish", "If true this Instruction waits until the dash is completed")]
    [Parameter("Mode", "Whether to use Cardinal Animations (4 clips for each direction) or a single one")]

    [Example(
        "The Damping value defines both the duration and the velocity rate at which the Character " +
        "moves when performing the Dash. To change the duration of the dash open the animation " +
        "curve window and move the last keyframe to the left to decrease the duration or to the " +
        "right to increase it."
    )]
    
    [Example(
        "The Damping value also defines the coefficient rate at which the Character moves " +
        "while performing the Dash. By default the Character starts with a coefficient of 0. " +
        "After 0.2 seconds it increases to 1 and goes back to 0 after 0.8 seconds. This curve " +
        "is evaluated while performing a Dash and the coefficient is extracted from the curve " +
        "and multiplied by the Speed to gradually change the rate at which the Character moves. " +
        "For this reason, it is recommended that the Damping stay between 0 and 1."
    )]
    
    [Keywords("Leap", "Blink", "Roll", "Flash")]
    [Image(typeof(IconCharacterDash), ColorTheme.Type.Blue)]

    [Serializable]
    public class InstructionCharacterNavigationDash : TInstructionCharacterNavigation
    {
        private const int DIRECTION_KEY = 5;
        
        [Serializable]
        public struct DashAnimation
        {
            public enum AnimationMode
            {
                CardinalAnimation,
                SingleAnimation
            }
            
            // EXPOSED MEMBERS: -------------------------------------------------------------------
            
            [SerializeField] private AnimationMode m_Mode;
        
            [SerializeField] private AnimationClip m_AnimationForward;
            [SerializeField] private AnimationClip m_AnimationBackward;
            [SerializeField] private AnimationClip m_AnimationRight;
            [SerializeField] private AnimationClip m_AnimationLeft;
            
            [SerializeField] private AnimationClip m_Animation;
            
            // PROPERTIES: ------------------------------------------------------------------------

            public AnimationMode Mode => this.m_Mode;

            public AnimationClip GetClip(float angle)
            {
                return this.m_Mode switch
                {
                    AnimationMode.CardinalAnimation => angle switch
                    {
                        <= 45f and >= -45f => this.m_AnimationForward,
                        < 135f and > 45f => this.m_AnimationLeft,
                        > -135f and < -45f => this.m_AnimationRight,
                        _ => this.m_AnimationBackward
                    },
                    AnimationMode.SingleAnimation => this.m_Animation,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private PropertyGetDirection m_Direction = GetDirectionCharactersMoving.Create;
        [SerializeField] private PropertyGetDecimal m_Velocity = new PropertyGetDecimal(10f);
        
        [SerializeField] private AnimationCurve m_Damping = new AnimationCurve(
            new Keyframe(0.0f, 0.0f, 0f, 0f), 
            new Keyframe(0.1f, 1.0f, 0f, 0f),
            new Keyframe(0.5f, 0.5f, 0f, 0f)
        );

        [SerializeField] private bool m_WaitToFinish = true;
        [SerializeField] private DashAnimation m_DashAnimation;

        [SerializeField] private float m_Speed = 1f;
        [SerializeField] private float m_TransitionIn = 0.1f;
        [SerializeField] private float m_TransitionOut = 0.25f;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Dash {this.m_Character} towards {this.m_Direction}";

        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override async Task Run(Args args)
        {
            Character character = this.m_Character.Get<Character>(args);
            if (character == null) return;
            if (character.Busy.AreLegsBusy) return;

            Vector3 direction = this.m_Direction.Get(args);
            if (direction == Vector3.zero) direction = character.transform.forward;
            
            float speed = (float) this.m_Velocity.Get(args);
            
            if (!character.Dash.CanDash()) return;
            Task task = character.Dash.Execute(direction, speed, this.m_Damping);

            character.Busy.MakeLegsBusy();
            float angle = Vector3.SignedAngle(
                direction, 
                character.transform.forward, 
                Vector3.up
            );

            AnimationClip animationClip = this.m_DashAnimation.GetClip(angle);

            if (animationClip != null)
            {
                ConfigGesture config = new ConfigGesture(
                    0f, animationClip.length, this.m_Speed, false,
                    this.m_TransitionIn, this.m_TransitionOut
                );
                
                _ = character.Gestures.CrossFade(animationClip, null, BlendMode.Blend, config, true);
                
                if (this.m_DashAnimation.Mode == DashAnimation.AnimationMode.SingleAnimation)
                {
                    character.Kernel.Facing.SetLayerDirection(
                        DIRECTION_KEY, 
                        direction,
                        Math.Max(animationClip.length - this.m_TransitionOut, 0f)
                    );
                }
            }

            if (this.m_WaitToFinish) await task;
        }
    }
}