using System;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class EquipmentSlot : TPolymorphicItem<EquipmentSlot>
    {
        [SerializeField] private Item m_Base;
        [SerializeField] private Bone m_Bone = new Bone(HumanBodyBones.RightHand);

        // PROPERTIES: ----------------------------------------------------------------------------

        public Item Base => this.m_Base;
        public IBone Bone => this.m_Bone;
        
        // STRING: --------------------------------------------------------------------------------

        public override string ToString()
        {
            return this.m_Base != null ? TextUtils.Humanize(this.m_Base.name) : "(none)";
        }
    }
}