using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Characters
{
    [Icon(RuntimePaths.GIZMOS + "GizmoWeapon.png")]
    [Serializable]
    public abstract class TWeapon : ScriptableObject, IWeapon
    {
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private UniqueID m_Id = new UniqueID();
        
        [SerializeField] private PropertyGetString m_Title = GetStringString.Create;
        [SerializeField] private PropertyGetString m_Description = GetStringTextArea.Create();

        [SerializeField] private PropertyGetSprite m_Icon = GetSpriteNone.Create;
        [SerializeField] private PropertyGetColor m_Color = GetColorColorsWhite.Create;

        [SerializeField] private Reaction m_Reaction;

        [SerializeField] private RunInstructionsList m_OnEquip = new RunInstructionsList();
        [SerializeField] private RunInstructionsList m_OnUnequip = new RunInstructionsList();

        // PROPERTIES: ----------------------------------------------------------------------------

        public IdString Id => this.m_Id.Get;

        public abstract Texture EditorIcon { get; }

        public Reaction Reaction => this.m_Reaction;

        // GETTERS: -------------------------------------------------------------------------------

        public string GetName(Args args) => this.m_Title.Get(args);
        public string GetDescription(Args args) => this.m_Description.Get(args);
        
        public Sprite GetSprite(Args args) => this.m_Icon.Get(args);
        public Color GetColor(Args args) => this.m_Color.Get(args);
        
        // RUNNERS: -------------------------------------------------------------------------------
        
        public async Task RunOnEquip(Args args)
        {
            await this.m_OnEquip.Run(args);
        }

        public async Task RunOnUnequip(Args args)
        {
            await this.m_OnUnequip.Run(args);
        }

        // PUBLIC METHODS: ------------------------------------------------------------------------

        public abstract TMunitionValue CreateMunition();

        public override string ToString() => TextUtils.Humanize(this.name);
    }
}