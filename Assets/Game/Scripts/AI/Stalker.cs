using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.AI
{
    public class Stalker : MonoBehaviour
    {
        // MEMBERS

        // PROPERTIES
        [ShowInInspector]
        public State CurrentState { get; set; }

        private const string IDLE_STATE_NAME = "Idle";
        private const string PATROL_STATE_NAME = "Patrol";
        private const string CHASING_STATE_NAME = "Chasing";
        private const string STUNNED_STATE_NAME = "Stunned";
        private const string ATTACKING_STATE_NAME = "Attacking";
        
        public GameObject IdleStateGameObject { get; set; }
        public GameObject PatrolStateGameObject { get; set; }
        public GameObject ChasingStateGameObject { get; set; }
        public GameObject StunnedStateGameObject { get; set; }
        public GameObject AttackingStateGameObject { get; set; }

        public enum State
        {
            Idle,
            Patrol,
            Chasing,
            Stunned,
            Attacking
        }

        private void Start()
        {
            SetupReferences();
            ChangeState(State.Idle);
        }

        private void SetupReferences()
        {
            if (IdleStateGameObject == null) IdleStateGameObject = Utilities.FindObjectInHierarchy(gameObject, IDLE_STATE_NAME);
            if (PatrolStateGameObject == null) PatrolStateGameObject = Utilities.FindObjectInHierarchy(gameObject, PATROL_STATE_NAME);
            if (ChasingStateGameObject == null) ChasingStateGameObject = Utilities.FindObjectInHierarchy(gameObject, CHASING_STATE_NAME);
            if (StunnedStateGameObject == null) StunnedStateGameObject = Utilities.FindObjectInHierarchy(gameObject, STUNNED_STATE_NAME);
            if (AttackingStateGameObject == null) AttackingStateGameObject = Utilities.FindObjectInHierarchy(gameObject, ATTACKING_STATE_NAME);
        }

        private void Update()
        {
            switch (CurrentState)
            {
                case State.Idle:
                    break;
                case State.Patrol:
                    break;
                case State.Chasing:
                    break;
                case State.Stunned:
                    break;
                case State.Attacking:
                    break;
            }
        }
        #region State Functions

        public void ChangeState(State newState)
        {
            // Call the onExit function for the old state
            switch (CurrentState)
            {
                case State.Idle:
                    OnExitIdleState();
                    break;
                
                case State.Patrol:
                    OnExitPatrolState();
                    break;
                
                case State.Chasing:
                    OnExitChasingState();
                    break;
                case State.Stunned:
                    OnExitStunnedState();
                    break;
                case State.Attacking:
                    OnExitAttackingState();
                    break;
            }
        
            // Change the current state to the new state
            CurrentState = newState;
        
            // Call the onEnter function for the new state
            switch (CurrentState)
            {
                case State.Idle:
                    OnEnterIdleState();
                    break;
                case State.Patrol:
                    OnEnterPatrolState();
                    break;
                case State.Chasing:
                    OnEnterChasingState();
                    break;
                case State.Stunned:
                    OnEnterStunnedState();
                    break;
                case State.Attacking:
                    OnEnterAttackingState();
                    break;
            }
        }

        #endregion
        #region Exit State

        private void OnExitIdleState()
        {
            IdleStateGameObject.SetActive(false);
        }
        private void OnExitPatrolState()
        {
            PatrolStateGameObject.SetActive(false);
        }
        private void OnExitChasingState()
        {
            ChasingStateGameObject.SetActive(false);
        }
        private void OnExitStunnedState()
        {
            StunnedStateGameObject.SetActive(false);
        }
        private void OnExitAttackingState()
        {
            AttackingStateGameObject.SetActive(false);
        }

        #endregion
        #region Enter State
        private void OnEnterIdleState()
        {
            IdleStateGameObject.SetActive(true);
        }
        private void OnEnterPatrolState()
        {
            PatrolStateGameObject.SetActive(true);
        }
        private void OnEnterChasingState()
        {
            ChasingStateGameObject.SetActive(true);
        }
        private void OnEnterStunnedState()
        {
            StunnedStateGameObject.SetActive(true);
        }
        private void OnEnterAttackingState()
        {
            AttackingStateGameObject.SetActive(true);
        }
        #endregion
        
    }
}
