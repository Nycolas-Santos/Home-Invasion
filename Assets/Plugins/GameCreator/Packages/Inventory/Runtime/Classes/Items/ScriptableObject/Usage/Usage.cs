using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class Usage
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private bool m_CanUse = false;
        [SerializeField] private bool m_ConsumeWhenUse = true;
        
        [SerializeField] private ItemConditions m_ConditionsCanUse = new ItemConditions();
        [SerializeField] private ItemInstructions m_InstructionsOnUse = new ItemInstructions();

        [SerializeField] private bool m_ExecuteFromParent = false;

        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private GameObject m_TemplateConditionsCanUse;
        [NonSerialized] private GameObject m_TemplateInstructionsOnUse;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool AllowUse => this.m_CanUse;
        public bool ConsumeWhenUse => this.m_ConsumeWhenUse;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public Usage()
        {
            this.m_TemplateInstructionsOnUse = null;
        }

        // CONDITION METHODS: ---------------------------------------------------------------------
        
        public static bool RunCanUse(Item item, Args args)
        {
            return RunCanUse(item, args, true);
        }
        
        private static bool RunCanUse(Item item, Args args, bool isLeaf)
        {
            if (isLeaf)
            {
                if (args?.Self == null) return false;
                if (!item.Usage.m_CanUse) return false;
            }
            
            if (item.Usage.m_ExecuteFromParent && item.Parent != null)
            {
                bool parentCanUse = RunCanUse(item.Parent, args, false);
                if (!parentCanUse) return false;
            }
            
            GameObject template = item.Usage.RequireConditionsCanUseTemplate();
            bool conditions = RunConditionsList.Check(
                args, template,
                new RunnerConfig
                {
                    Name = $"Can Use {TextUtils.Humanize(item.name)}",
                    Location = new RunnerLocationParent(args.Self.transform)
                }
            );

            return conditions;
        }
        
        // USAGE METHODS: -------------------------------------------------------------------------
        
        public static async Task RunOnUse(Item item, Args args)
        {
            try
            {
                if (item.Usage.m_ExecuteFromParent && item.Parent != null)
                {
                    await RunOnUse(item.Parent, args);
                }
                
                GameObject template = item.Usage.RequireInstructionsOnUseTemplate();
                await RunInstructionsList.Run(
                    args, template, 
                    new RunnerConfig
                    {
                        Name = $"On Use {TextUtils.Humanize(item.name)}",
                        Location = new RunnerLocationParent(args.Self.transform)
                    }
                );
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString(), args.Self);
            }
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------

        private GameObject RequireConditionsCanUseTemplate()
        {
            if (this.m_TemplateConditionsCanUse == null)
            {
                this.m_TemplateConditionsCanUse = RunConditionsList.CreateTemplate(
                    this.m_ConditionsCanUse.List
                );
            }

            return this.m_TemplateConditionsCanUse;
        }
        
        private GameObject RequireInstructionsOnUseTemplate()
        {
            if (this.m_TemplateInstructionsOnUse == null)
            {
                this.m_TemplateInstructionsOnUse = RunInstructionsList.CreateTemplate(
                    this.m_InstructionsOnUse.List
                );
            }

            return this.m_TemplateInstructionsOnUse;
        }
    }
}