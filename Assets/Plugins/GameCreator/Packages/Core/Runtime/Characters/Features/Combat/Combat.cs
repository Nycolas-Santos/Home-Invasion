using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Characters
{
    [Serializable]
    public class Combat
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [NonSerialized] private Target m_Target;
        
        [NonSerialized] private Dictionary<int, Weapon> m_Weapons;
        [NonSerialized] private Dictionary<int, IMunition> m_Munitions;
        [NonSerialized] private Dictionary<int, IStance> m_Stances;
        [NonSerialized] private Character m_Character;

        // PROPERTIES: ----------------------------------------------------------------------------

        public GameObject Target
        {
            get => this.m_Target.On;
            set => this.m_Target.On = value;
        }
        
        public Weapon[] Weapons
        {
            get
            {
                List<Weapon> weapons = new List<Weapon>();
                foreach (KeyValuePair<int, Weapon> entry in this.m_Weapons)
                {
                    weapons.Add(entry.Value);
                }

                return weapons.ToArray();
            }
        }

        public IMunition[] Munitions
        {
            get
            {
                List<IMunition> munitions = new List<IMunition>();
                foreach (KeyValuePair<int, IMunition> entry in this.m_Munitions)
                {
                    munitions.Add(entry.Value);
                }

                return munitions.ToArray();
            }
        }

        // EVENTS: --------------------------------------------------------------------------------

        public event Action<IWeapon, GameObject> EventEquip;
        public event Action<IWeapon, GameObject> EventUnequip;
        
        // CONSTRUCTOR: ---------------------------------------------------------------------------

        public Combat()
        {
            this.m_Target = new Target();
            this.m_Weapons = new Dictionary<int, Weapon>();
            this.m_Munitions = new Dictionary<int, IMunition>();
            this.m_Stances = new Dictionary<int, IStance>();
        }
        
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
        {
            foreach (KeyValuePair<int, IStance> entry in this.m_Stances)
            {
                entry.Value.OnEnable(this.m_Character);
            }
        }

        internal void OnDisable()
        {
            foreach (KeyValuePair<int, IStance> entry in this.m_Stances)
            {
                entry.Value.OnDisable(this.m_Character);
            }
        }
        
        // UPDATE METHODS: ------------------------------------------------------------------------

        internal void OnLateUpdate()
        {
            foreach (KeyValuePair<int, IStance> entry in this.m_Stances)
            {
                entry.Value.OnUpdate();
            }
        }

        // GETTERS: -------------------------------------------------------------------------------

        public TMunitionValue RequestMunition(IWeapon weapon)
        {
            if (weapon == null) return null;
            if (this.m_Munitions.TryGetValue(weapon.Id.Hash, out IMunition munition))
            {
                return munition.Value;
            }

            munition = new Munition(weapon.Id.Hash, weapon.CreateMunition()); 
            this.m_Munitions.Add(weapon.Id.Hash, munition);

            return munition.Value;
        }

        public T RequestStance<T>() where T : IStance, new()
        {
            int stanceId = typeof(T).GetHashCode();
            if (this.m_Stances.TryGetValue(stanceId, out IStance stance))
            {
                return (T) stance;
            }

            T newStance = new T();
            newStance.OnEnable(this.m_Character);

            this.m_Stances.Add(stanceId, newStance);
            return newStance;
        }
        
        public ReactionOutput GetReaction(ReactionInput input, Args args, IReaction reaction)
        {
            if (reaction?.CanRun(this.m_Character, args, input) ?? false)
            {
                return reaction.Run(this.m_Character, args, input);
            }

            foreach (Weapon weapon in this.m_Character.Combat.Weapons)
            {
                if (weapon.Asset.Reaction == null) continue;
                if (!weapon.Asset.Reaction.CanRun(this.m_Character, args, input)) continue;
                
                return weapon.Asset.Reaction.Run(this.m_Character, args, input);
            }

            Reaction defaultReaction = this.m_Character.Animim.Reaction;
            if (defaultReaction == null) return ReactionOutput.None;

            return defaultReaction.CanRun(this.m_Character, args, input) 
                ? defaultReaction.Run(this.m_Character, args, input)
                : ReactionOutput.None;
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public bool IsEquipped(IWeapon weapon)
        {
            return weapon != null && this.m_Weapons.ContainsKey(weapon.Id.Hash);
        }

        public async Task Equip(IWeapon asset, GameObject instance, Args args)
        {
            if (asset == null) return;
            if (this.IsEquipped(asset)) return;
            
            Weapon weapon = new Weapon(asset, instance);
            this.m_Weapons.Add(asset.Id.Hash, weapon);

            if (!this.m_Munitions.ContainsKey(asset.Id.Hash))
            {
                Munition munition = new Munition(asset.Id.Hash, asset.CreateMunition());
                this.m_Munitions.Add(asset.Id.Hash, munition);
            }

            await asset.RunOnEquip(args);
            this.EventEquip?.Invoke(asset, instance);
        }

        public async Task Unequip(IWeapon asset, Args args)
        {
            if (asset == null) return;
            if (!this.IsEquipped(asset)) return;

            Weapon weapon = this.m_Weapons[asset.Id.Hash];
            this.m_Weapons.Remove(asset.Id.Hash);

            await asset.RunOnUnequip(args);
            this.EventUnequip?.Invoke(asset, weapon.Instance);
        }

        public GameObject GetProp(IWeapon asset)
        {
            if (asset == null) return null;
            return this.m_Weapons.TryGetValue(asset.Id.Hash, out Weapon weapon)
                ? weapon.Instance
                : null;
        }
    }
}