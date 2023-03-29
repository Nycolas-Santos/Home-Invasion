using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using Event = GameCreator.Runtime.VisualScripting.Event;

namespace NinjutsuGames.StateMachine.Runtime
{
    [AddComponentMenu("")]
    public class TriggerRunner : Trigger
    {
        public void Setup(Event triggerEvent, Args args, Action<Args> OnAfterProcess, Action OnProcess)
        {
            hideFlags = HideFlags.HideInInspector | HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy;
            enabled = false;
            
            m_TriggerEvent = triggerEvent;
            m_Instructions = new InstructionList();
            Awake();
            EventBeforeExecute += () => OnAfterProcess(args);
            EventAfterExecute += OnProcess;
            gameObject.SetActive(true);
            enabled = true;
        }
    }
}