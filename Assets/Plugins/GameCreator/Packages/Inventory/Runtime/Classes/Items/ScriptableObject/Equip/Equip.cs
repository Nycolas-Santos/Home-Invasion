using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class Equip
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private bool m_IsEquippable;
        [SerializeField] private GameObject m_Prefab;

        [SerializeField] private ItemConditions m_ConditionsEquip = new ItemConditions();
        [SerializeField] private ItemInstructions m_InstructionsOnEquip = new ItemInstructions();
        [SerializeField] private ItemInstructions m_InstructionsOnUnequip = new ItemInstructions();
        
        [SerializeField] private bool m_ExecuteFromParent;
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private GameObject m_TemplateConditionsEquip;
        [NonSerialized] private GameObject m_TemplateInstructionsOnEquip;
        [NonSerialized] private GameObject m_TemplateInstructionsOnUnequip;

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsEquippable => this.m_IsEquippable;
        public GameObject Prefab => this.m_Prefab;

        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public Equip()
        {
            this.m_TemplateConditionsEquip = null;
            this.m_TemplateInstructionsOnEquip = null;
            this.m_TemplateInstructionsOnUnequip = null;
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------ 
        
        public static bool RunCanEquip(Item item, Args args)
        {
            if (item.Equip.m_ExecuteFromParent && item.Parent != null)
            {
                bool parentCanEquip = RunCanEquip(item.Parent, args);
                if (!parentCanEquip) return false;
            }

            GameObject template = item.Equip.RequireConditionsEquipTemplate();
            bool conditions = RunConditionsList.Check(
                args, template,
                new RunnerConfig
                {
                    Name = $"Can Equip {TextUtils.Humanize(item.name)}",
                    Location = new RunnerLocationParent(args.Self.transform)
                }
            );

            return conditions;
        }
        
        public static async Task RunOnEquip(Item item, Args args)
        {
            try
            {
                if (item.Equip.m_ExecuteFromParent && item.Parent != null)
                {
                    await RunOnEquip(item.Parent, args);
                }
                
                GameObject template = item.Equip.RequireInstructionsOnEquipTemplate();
                await RunInstructionsList.Run(
                    args, template, 
                    new RunnerConfig
                    {
                        Name = $"On Equip {TextUtils.Humanize(item.name)}",
                        Location = new RunnerLocationParent(args.Self.transform)
                    }
                );
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString(), args.Self);
            }
        }
        
        public static async Task RunOnUnequip(Item item, Args args)
        {
            try
            {
                if (item.Equip.m_ExecuteFromParent && item.Parent != null)
                {
                    await RunOnUnequip(item.Parent, args);
                }
                
                GameObject template = item.Equip.RequireInstructionsOnUnequipTemplate();
                await RunInstructionsList.Run(
                    args, template, 
                    new RunnerConfig
                    {
                        Name = $"Can Unequip {TextUtils.Humanize(item.name)}",
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
        
        private GameObject RequireConditionsEquipTemplate()
        {
            if (this.m_TemplateConditionsEquip == null)
            {
                this.m_TemplateConditionsEquip = RunConditionsList.CreateTemplate(
                    this.m_ConditionsEquip.List
                );
            }

            return this.m_TemplateConditionsEquip;
        }
        
        private GameObject RequireInstructionsOnEquipTemplate()
        {
            if (this.m_TemplateInstructionsOnEquip == null)
            {
                this.m_TemplateInstructionsOnEquip = RunInstructionsList.CreateTemplate(
                    this.m_InstructionsOnEquip.List
                );
            }

            return this.m_TemplateInstructionsOnEquip;
        }
        
        private GameObject RequireInstructionsOnUnequipTemplate()
        {
            if (this.m_TemplateInstructionsOnUnequip == null)
            {
                this.m_TemplateInstructionsOnUnequip = RunInstructionsList.CreateTemplate(
                    this.m_InstructionsOnUnequip.List
                );
            }

            return this.m_TemplateInstructionsOnUnequip;
        }
    }
}