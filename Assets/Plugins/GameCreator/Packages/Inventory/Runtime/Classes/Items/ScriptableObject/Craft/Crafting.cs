using System;
using System.Collections.Generic;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class Crafting
    {
        #if UNITY_EDITOR
        
        [UnityEditor.InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode()
        {
            EventCraft = null;
            EventDismantle = null;

            LastItemCrafted = null;
            LastItemDismantled = null;
        }
        
        #endif

        public static RuntimeItem LastItemCrafted;
        public static RuntimeItem LastItemDismantled;
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Ingredient[] m_Ingredients = Array.Empty<Ingredient>();

        [SerializeField] private bool m_CanCraft;
        [SerializeField] private ItemConditions m_ConditionsCraft = new ItemConditions();
        [SerializeField] private ItemInstructions m_InstructionsOnCraft = new ItemInstructions();

        [SerializeField] private bool m_CanDismantle;
        [SerializeField] private ItemConditions m_ConditionsDismantle = new ItemConditions();
        [SerializeField] private ItemInstructions m_InstructionsOnDismantle = new ItemInstructions();
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private GameObject m_TemplateConditionsCraft;
        [NonSerialized] private GameObject m_TemplateInstructionsOnCraft;
        
        [NonSerialized] private GameObject m_TemplateConditionsDismantle;
        [NonSerialized] private GameObject m_TemplateInstructionsOnDismantle;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Ingredient[] Ingredients => this.m_Ingredients;

        public bool AllowToCraft => this.m_CanCraft;
        public bool AllowToDismantle => this.m_CanDismantle;
        
        // EVENTS: --------------------------------------------------------------------------------

        public static event Action EventCraft;
        public static event Action EventDismantle;

        // CRAFT METHODS: -------------------------------------------------------------------------
        
        public static bool CanCraft(Item item, Bag inputBag, Bag outputBag)
        {
            if (item == null || inputBag == null || outputBag == null) return false;
            if (!item.Crafting.m_CanCraft) return false;

            if (item.Crafting.m_TemplateConditionsCraft == null)
            {
                item.Crafting.m_TemplateConditionsCraft = RunConditionsList.CreateTemplate(
                    item.Crafting.m_ConditionsCraft.List
                );
            }
            
            bool canCraft = RunConditionsList.Check(
                new Args(inputBag.gameObject, outputBag.gameObject),
                item.Crafting.m_TemplateConditionsCraft,
                new RunnerConfig
                {
                    Name = $"Can Craft {TextUtils.Humanize(item.name)}",
                    Location = new RunnerLocationParent(outputBag.Wearer != null 
                        ? outputBag.Wearer.transform 
                        : outputBag.transform
                    ) 
                }
            );
            
            if (!canCraft) return false;
            
            int ingredientsLength = item.Crafting.Ingredients.Length;
            for (int i = 0; i < ingredientsLength; ++i)
            {
                Ingredient ingredient = item.Crafting.Ingredients[i];
                if (inputBag.Content.ContainsType(ingredient.Item, ingredient.Amount)) continue;
                
                return false;
            }

            return outputBag.Content.CanAddType(item, true);
        }
        
        public static RuntimeItem Craft(Item item, Bag inputBag, Bag outputBag)
        {
            if (!CanCraft(item, inputBag, outputBag)) return null;

            int ingredientsLength = item.Crafting.Ingredients.Length;
            List<RuntimeItem> removeRuntimeItemList = new List<RuntimeItem>();
            
            for (int i = 0; i < ingredientsLength; ++i)
            {
                Ingredient ingredient = item.Crafting.Ingredients[i];
                for (int j = 0; j < ingredient.Amount; ++j)
                {
                    RuntimeItem removeRuntimeItem = inputBag.Content.RemoveType(ingredient.Item);
                    if (removeRuntimeItem != null)
                    {
                        removeRuntimeItemList.Add(removeRuntimeItem);
                        continue;
                    }

                    foreach (RuntimeItem restoreRuntimeItem in removeRuntimeItemList)
                    {
                        inputBag.Content.Add(restoreRuntimeItem, true);
                    }
                    
                    return null;
                }
            }

            RuntimeItem craftRuntimeItem = outputBag.Content.AddType(item, true);
            if (craftRuntimeItem == null)
            {
                foreach (RuntimeItem restoreRuntimeItem in removeRuntimeItemList)
                {
                    inputBag.Content.Add(restoreRuntimeItem, true);
                }

                return null;
            }

            LastItemCrafted = craftRuntimeItem;
            
            if (item.Crafting.m_TemplateInstructionsOnCraft == null)
            {
                item.Crafting.m_TemplateInstructionsOnCraft = RunInstructionsList.CreateTemplate(
                    item.Crafting.m_InstructionsOnCraft.List
                );
            }

            try
            {
                _ = RunInstructionsList.Run(
                    new Args(inputBag.gameObject, outputBag.gameObject),
                    item.Crafting.m_TemplateInstructionsOnCraft,
                    new RunnerConfig
                    {
                        Name = $"On Craft {TextUtils.Humanize(item.name)}",
                        Location = new RunnerLocationParent(outputBag.Wearer != null 
                            ? outputBag.Wearer.transform 
                            : outputBag.transform
                        ) 
                    }
                );
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString(), inputBag.gameObject);
            }

            EventCraft?.Invoke();
            return craftRuntimeItem;
        }

        // DISMANTLE METHODS: ---------------------------------------------------------------------

        public static bool CanDismantle(Item item, Bag inputBag, Bag outputBag)
        {
            if (item == null || inputBag == null || outputBag == null) return false;
            if (!inputBag.Content.ContainsType(item, 1)) return false;
            if (!item.Crafting.m_CanDismantle) return false;
            
            if (item.Crafting.m_TemplateConditionsDismantle == null)
            {
                item.Crafting.m_TemplateConditionsDismantle = RunConditionsList.CreateTemplate(
                    item.Crafting.m_ConditionsDismantle.List
                );
            }
            
            bool canDismantle = RunConditionsList.Check(
                new Args(inputBag.gameObject, outputBag.gameObject),
                item.Crafting.m_TemplateConditionsDismantle,
                new RunnerConfig
                {
                    Name = $"Can Dismantle {TextUtils.Humanize(item.name)}",
                    Location = new RunnerLocationParent(outputBag.Wearer != null 
                        ? outputBag.Wearer.transform 
                        : outputBag.transform
                    ) 
                }
            );

            return canDismantle;
        }
        
        public static RuntimeItem[] Dismantle(Item item, Bag inputBag, Bag outputBag, float chance)
        {
            if (!CanDismantle(item, inputBag, outputBag)) return null;
            
            RuntimeItem removeRuntimeItem = inputBag.Content.RemoveType(item);
            return removeRuntimeItem != null 
                ? ProcessDismantle(removeRuntimeItem, inputBag, outputBag, chance) 
                : null;
        }
        
        public static RuntimeItem[] Dismantle(RuntimeItem runtimeItem, Bag inputBag, Bag outputBag, float chance)
        {
            if (!CanDismantle(runtimeItem.Item, inputBag, outputBag)) return null;
            
            RuntimeItem removeRuntimeItem = inputBag.Content.Remove(runtimeItem);
            return ProcessDismantle(removeRuntimeItem, inputBag, outputBag, chance);
        }
        
        private static RuntimeItem[] ProcessDismantle(RuntimeItem removedItem, Bag inputBag, Bag outputBag, float chance)
        {
            if (removedItem?.Item == null) return null;
            
            int ingredientsLength = removedItem.Item.Crafting.Ingredients.Length;
            List<RuntimeItem> dismantleRuntimeItems = new List<RuntimeItem>();
            
            for (int i = 0; i < ingredientsLength; ++i)
            {
                Ingredient ingredient = removedItem.Item.Crafting.Ingredients[i];
                for (int j = 0; j < ingredient.Amount; ++j)
                {
                    float randomChance = UnityEngine.Random.value;
                    if (randomChance > chance) continue;
                    
                    RuntimeItem dismantleRuntimeItem = outputBag
                        .Content.AddType(ingredient.Item, true);
                    
                    if (dismantleRuntimeItem != null)
                    {
                        dismantleRuntimeItems.Add(dismantleRuntimeItem);
                    }
                }
            }
            
            LastItemDismantled = removedItem;

            if (removedItem.Item.Crafting.m_TemplateInstructionsOnDismantle == null)
            {
                removedItem.Item.Crafting.m_TemplateInstructionsOnDismantle = RunInstructionsList.CreateTemplate(
                    removedItem.Item.Crafting.m_InstructionsOnDismantle.List
                );
            }
            
            try
            {
                _ = RunInstructionsList.Run(
                    new Args(inputBag.gameObject, outputBag.gameObject),
                    removedItem.Item.Crafting.m_TemplateInstructionsOnDismantle,
                    new RunnerConfig
                    {
                        Name = $"On Dismantle {TextUtils.Humanize(removedItem.Item.name)}",
                        Location = new RunnerLocationParent(outputBag.Wearer != null 
                            ? outputBag.Wearer.transform 
                            : outputBag.transform
                        ) 
                    }
                );
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.ToString(), inputBag.gameObject);
            }

            EventDismantle?.Invoke();
            return dismantleRuntimeItems.ToArray();
        }
    }
}