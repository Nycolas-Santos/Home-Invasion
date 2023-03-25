using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Characters
{
    [Serializable]
    public class Dash
    {
        private static readonly int HASH = Tween.GetHash(typeof(Transform), "position");
        
        private const int DASH_PRIORITY = 2;
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Character m_Character;
        
        [NonSerialized] private bool m_HasDodged;
        
        [NonSerialized] private float m_LastDashFinishTime = -100f;
        [NonSerialized] private int m_NumDashes;

        [NonSerialized] private Vector3 m_Direction;
        [NonSerialized] private float m_Speed;
        [NonSerialized] private AnimationCurve m_Damping;

        // PROPERTIES: ----------------------------------------------------------------------------

        [field: NonSerialized] public bool IsDashing { get; private set; }

        public bool IsDodge => this.IsDashing && this.m_Character.Combat.Invincibility.IsInvincible;

        // EVENTS: --------------------------------------------------------------------------------

        public event Action EventDashStart;
        public event Action EventDashFinish;
        
        public event Action EventDodge;

        // INITIALIZE METHODS: --------------------------------------------------------------------
        
        internal void OnStartup(Character character)
        {
            this.m_Character = character;
        }
        
        internal void AfterStartup(Character character)
        { }

        internal void OnDispose(Character character)
        {
            this.m_Character = character;
        }

        internal void OnEnable()
        { }

        internal void OnDisable()
        { }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool CanDash()
        {
            if (this.m_Character.Busy.AreLegsBusy) return false;

            bool canDashInAir = this.m_Character.Motion.DashInAir;
            if (!canDashInAir && !this.m_Character.Driver.IsGrounded) return false;

            if (this.IsDashing) return false;

            float resetTime = this.m_LastDashFinishTime + this.m_Character.Motion.DashCooldown;
            if (this.m_Character.Time.Time >= resetTime) return true;
            
            return this.m_NumDashes <= this.m_Character.Motion.DashInSuccession;
        }
        
        public async Task Execute(Vector3 direction, float speed, AnimationCurve damping)
        {
            this.m_HasDodged = false;
            
            this.m_Direction = direction.normalized;
            this.m_Speed = speed;
            this.m_Damping = damping;
            
            float resetTime = this.m_LastDashFinishTime + this.m_Character.Motion.DashCooldown;
            this.m_NumDashes = this.m_Character.Time.Time < resetTime 
                ? this.m_NumDashes + 1
                : 1;
            
            this.IsDashing = true;
            this.EventDashStart?.Invoke();

            float duration = damping.length > 0
                ? damping[damping.length - 1].time
                : 0f;

            TweenInput<float> input = new TweenInput<float>(
                0f, 1f, duration,
                this.OnDashUpdate,
                HASH,
                Easing.Type.Linear
            );
            
            input.EventFinish += this.OnDashFinish;
            
            Tween.To(this.m_Character.gameObject, input);
            while (this.IsDashing && !ApplicationManager.IsExiting) await Task.Yield();
            
            this.m_LastDashFinishTime = this.m_Character.Time.Time;
        }

        public void Cancel()
        {
            if (!this.IsDashing) return;
            Tween.Cancel(this.m_Character.gameObject, HASH);
        }

        // CALLBACKS: -----------------------------------------------------------------------------

        public void OnDodge(Args args)
        {
            if (this.m_HasDodged) return;

            foreach (Weapon weapon in this.m_Character.Combat.Weapons)
            {
                weapon.Asset.RunOnDodge(this.m_Character, args);
            }
            
            this.EventDodge?.Invoke();
            this.m_HasDodged = true;
        }
        
        private void OnDashUpdate(float valueStart, float valueEnd, float t)
        {
            float damping = this.m_Damping.Evaluate(t); 
            Vector3 value = this.m_Direction * (damping * this.m_Speed);
            
            this.m_Character.Motion.MoveToDirection(
                value, 
                Space.World,
                DASH_PRIORITY
            );
        }

        private void OnDashFinish(bool isComplete)
        {
            this.m_Character.Motion.StopToDirection(DASH_PRIORITY);
            
            this.m_Character.Busy.RemoveLegsBusy();
            this.m_Character.Motion.StopToDirection();
            
            this.IsDashing = false;
            this.EventDashFinish?.Invoke();
        }
    }
}