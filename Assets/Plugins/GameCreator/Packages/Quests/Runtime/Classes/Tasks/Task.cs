using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Quests
{
    [Serializable]
    public class Task
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private TaskType m_Completion = TaskType.SubtasksInSequence;
        [SerializeField] private bool m_IsHidden;

        [SerializeField] private PropertyGetString m_Name = GetStringString.Create;
        [SerializeField] private PropertyGetString m_Description = GetStringTextArea.Create();

        [SerializeField] private PropertyGetColor m_Color = GetColorColorsWhite.Create;
        [SerializeField] private PropertyGetSprite m_Sprite = GetSpriteNone.Create;

        [SerializeField] private ProgressType m_UseCounter = ProgressType.None;
        [SerializeField] private PropertyGetDecimal m_CountTo = new PropertyGetDecimal(3);

        [SerializeField] private PropertyGetDecimal m_ValueFrom = GetDecimalGlobalName.Create;
        [SerializeReference] private VisualScripting.Event m_CheckWhen = new EventOnLateUpdate();

        [SerializeField] private InstructionList m_OnActivate = new InstructionList();
        [SerializeField] private InstructionList m_OnDeactivate = new InstructionList();
        [SerializeField] private InstructionList m_OnComplete = new InstructionList();
        [SerializeField] private InstructionList m_OnAbandon = new InstructionList();
        [SerializeField] private InstructionList m_OnFail = new InstructionList();

        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private GameObject m_TemplateOnActivate;
        [NonSerialized] private GameObject m_TemplateOnDeactivate;

        [NonSerialized] private GameObject m_TemplateOnComplete;
        [NonSerialized] private GameObject m_TemplateOnAbandon;
        [NonSerialized] private GameObject m_TemplateOnFail;

        [NonSerialized] private GameObject m_TemplateTrigger;

        // PROPERTIES: ----------------------------------------------------------------------------

        public TaskType Completion => this.m_Completion;
        
        public bool IsHidden => this.m_IsHidden;
        public ProgressType UseCounter => this.m_UseCounter;

        public string MaximumValueString => this.m_CountTo.ToString();
        public PropertyGetDecimal ValueFrom => this.m_ValueFrom;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public string GetName(Args args) => this.m_Name.Get(args);
        public string GetDescription(Args args) => this.m_Description.Get(args);

        public Color GetColor(Args args) => this.m_Color.Get(args);
        public Sprite GetSprite(Args args) => this.m_Sprite.Get(args);

        public double GetCountTo(Args args) => this.m_UseCounter switch
        {
            ProgressType.None => 0,
            ProgressType.Value => this.m_CountTo.Get(args),
            ProgressType.Property => this.m_CountTo.Get(args),
            _ => throw new ArgumentOutOfRangeException()
        };

        public async System.Threading.Tasks.Task RunOnActivate(Args args)
        {
            if (this.m_TemplateOnActivate == null)
            {
                this.m_TemplateOnActivate = RunInstructionsList.CreateTemplate(
                    this.m_OnActivate
                );
            }
            
            await RunInstructionsList.Run(
                args.Clone, this.m_TemplateOnActivate, 
                new RunnerConfig
                {
                    Name = "On Activate Task",
                    Location = new RunnerLocationParent(args.ComponentFromSelf<Transform>())
                }
            );
        }
        
        public async System.Threading.Tasks.Task RunOnDeactivate(Args args)
        {
            if (this.m_TemplateOnDeactivate == null)
            {
                this.m_TemplateOnDeactivate = RunInstructionsList.CreateTemplate(
                    this.m_OnDeactivate
                );
            }
            
            await RunInstructionsList.Run(
                args.Clone, this.m_TemplateOnDeactivate, 
                new RunnerConfig
                {
                    Name = "On Deactivate Task",
                    Location = new RunnerLocationParent(args.ComponentFromSelf<Transform>())
                }
            );
        }
        
        public async System.Threading.Tasks.Task RunOnComplete(Args args)
        {
            if (this.m_TemplateOnComplete == null)
            {
                this.m_TemplateOnComplete = RunInstructionsList.CreateTemplate(
                    this.m_OnComplete
                );
            }
            
            await RunInstructionsList.Run(
                args.Clone, this.m_TemplateOnComplete, 
                new RunnerConfig
                {
                    Name = "On Complete Task",
                    Location = new RunnerLocationParent(args.ComponentFromSelf<Transform>())
                }
            );
        }
        
        public async System.Threading.Tasks.Task RunOnAbandon(Args args)
        {
            if (this.m_TemplateOnAbandon == null)
            {
                this.m_TemplateOnAbandon = RunInstructionsList.CreateTemplate(
                    this.m_OnAbandon
                );
            }
            
            await RunInstructionsList.Run(
                args.Clone, this.m_TemplateOnAbandon, 
                new RunnerConfig
                {
                    Name = "On Abandon Task",
                    Location = new RunnerLocationParent(args.ComponentFromSelf<Transform>())
                }
            );
        }
        
        public async System.Threading.Tasks.Task RunOnFail(Args args)
        {
            if (this.m_TemplateOnFail == null)
            {
                this.m_TemplateOnFail = RunInstructionsList.CreateTemplate(
                    this.m_OnFail
                );
            }
            
            await RunInstructionsList.Run(
                args.Clone, this.m_TemplateOnFail, 
                new RunnerConfig
                {
                    Name = "On Fail Task",
                    Location = new RunnerLocationParent(args.ComponentFromSelf<Transform>())
                }
            );
        }

        public Trigger CreateCheckWhen(InstructionList instructions)
        {
            if (this.m_TemplateTrigger == null)
            {
                this.m_TemplateTrigger = new GameObject
                {
                    name = "Task Detection",
                    hideFlags = HideFlags.HideInHierarchy
                };
                
                this.m_TemplateTrigger.SetActive(false);
                this.m_TemplateTrigger.Add<Trigger>();
            }
            
            Trigger.Reconfigure(
                this.m_TemplateTrigger.Get<Trigger>(), 
                this.m_CheckWhen, 
                instructions
            );
            
            GameObject instance = UnityEngine.Object.Instantiate(this.m_TemplateTrigger);
            instance.hideFlags = HideFlags.None;
            
            instance.SetActive(true);
            return instance.Get<Trigger>();
        }
        
        // STRING: --------------------------------------------------------------------------------

        public override string ToString()
        {
            string name = this.m_Name.ToString();
            return !string.IsNullOrEmpty(name) ? name : "(no name)";
        }
    }
}