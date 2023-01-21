using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Dialogue
{
    [Serializable]
    public class Expression : TPolymorphicItem<Expression>, IExpression
    {
        public const string DEFAULT_NAME = "my-expression";
        public const string NAME_ID = nameof(m_Id);
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private IdString m_Id = new IdString(DEFAULT_NAME);
        [SerializeField] private Sprite m_Sprite;
        
        [SerializeField] private InstructionList m_InstructionsOnStart = new InstructionList();
        [SerializeField] private InstructionList m_InstructionsOnEnd = new InstructionList();
        
        [SerializeField] private SpeechSkin m_OverrideSpeechSkin;
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private GameObject m_TemplateOnStart;
        [NonSerialized] private GameObject m_TemplateOnEnd;
        
        // PROPERTIES: ----------------------------------------------------------------------------

        public IdString Id => this.m_Id;

        public Sprite Sprite => this.m_Sprite;
        
        public SpeechSkin OverrideSpeechSkin => this.m_OverrideSpeechSkin;
        
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public async Task OnStart(Args args)
        {
            if (this.m_TemplateOnStart == null)
            {
                this.m_TemplateOnStart = RunInstructionsList.CreateTemplate(
                    this.m_InstructionsOnStart
                );
            }
            
            await RunInstructionsList.Run(
                args.Clone, this.m_TemplateOnStart, 
                new RunnerConfig
                {
                    Name = $"On Start Expression {TextUtils.Humanize(this.m_Id.String)}",
                    Location = new RunnerLocationPosition(
                        args.Self != null ? args.Self.transform.position : Vector3.zero, 
                        args.Self != null ? args.Self.transform.rotation : Quaternion.identity
                    )
                }
            );
        }

        public async Task OnEnd(Args args)
        {
            if (this.m_TemplateOnEnd == null)
            {
                this.m_TemplateOnEnd = RunInstructionsList.CreateTemplate(
                    this.m_InstructionsOnEnd
                );
            }
            
            await RunInstructionsList.Run(
                args.Clone, this.m_TemplateOnEnd, 
                new RunnerConfig
                {
                    Name = $"On End Expression {TextUtils.Humanize(this.m_Id.String)}",
                    Location = new RunnerLocationPosition(
                        args.Self != null ? args.Self.transform.position : Vector3.zero, 
                        args.Self != null ? args.Self.transform.rotation : Quaternion.identity
                    )
                }
            );
        }
        
        // STRING: --------------------------------------------------------------------------------

        public override string ToString() => this.m_Id.String;
    }
}