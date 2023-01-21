using System;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Characters
{
    public class ToolCombat : VisualElement
    {
        private const string USS_PATH = EditorPaths.CHARACTERS + "StyleSheets/Combat";

        private const string NAME_ROOT = "GC-Characters-Combat-Root";
        private const string NAME_HEAD = "GC-Characters-Combat-Head";
        private const string NAME_BODY = "GC-Characters-Combat-Body";
        
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private readonly VisualElement m_Root;
        [NonSerialized] private readonly VisualElement m_Head;
        [NonSerialized] private readonly VisualElement m_Body;

        [NonSerialized] private readonly Character m_Character;
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------
        
        public ToolCombat(Character character)
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode) return;
            if (character == null) return;
            
            StyleSheet[] sheets = StyleSheetUtils.Load(USS_PATH);
            foreach (StyleSheet styleSheet in sheets) this.styleSheets.Add(styleSheet);

            this.m_Character = character;
            
            this.m_Character.Combat.EventEquip -= this.Refresh;
            this.m_Character.Combat.EventUnequip -= this.Refresh;
            
            this.m_Character.Combat.EventEquip += this.Refresh;
            this.m_Character.Combat.EventUnequip += this.Refresh;

            this.m_Root = new VisualElement { name = NAME_ROOT };
            this.m_Head = new VisualElement { name = NAME_HEAD };
            this.m_Body = new VisualElement { name = NAME_BODY };
            
            this.m_Root.Add(this.m_Head);
            this.m_Root.Add(this.m_Body);
            this.Add(this.m_Root);

            this.Refresh(null, null);
        }

        private void Refresh(IWeapon weapon, GameObject instance)
        {
            this.m_Body.Clear();

            Weapon[] weapons = this.m_Character.Combat.Weapons;
            this.m_Root.style.display = weapons.Length > 0
                ? DisplayStyle.Flex
                : DisplayStyle.None; 
            
            foreach (Weapon reference in weapons)
            {
                TMunitionValue munition = this.m_Character.Combat.RequestMunition(reference.Asset);
                ToolCombatItem item = new ToolCombatItem(reference, munition);
                this.m_Body.Add(item);
            }
        }
    }
}