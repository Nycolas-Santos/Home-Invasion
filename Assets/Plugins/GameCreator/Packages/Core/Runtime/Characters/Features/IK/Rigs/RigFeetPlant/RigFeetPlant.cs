using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Characters.IK
{
    [Title("Align Feet with Ground")]
    [Category("Align Feet with Ground")]
    [Image(typeof(IconFootprint), ColorTheme.Type.Green)]
    
    [Description(
        "IK system that allows the Character to correctly align their feet to uneven terrain. " +
        "It also avoids character's feet from penetrating the floor. Requires a humanoid character"
    )]
    
    [Serializable]
    public class RigFeetPlant : TRigAnimatorIK
    {
        // CONSTANTS: -----------------------------------------------------------------------------

        public const string RIG_NAME = "RigFeetPlant";

        // EXPOSED MEMBERS: -----------------------------------------------------------------------
        
        [SerializeField] private float m_FootOffset;
        [SerializeField] private LayerMask m_FootMask = -1;

        // MEMBERS: -------------------------------------------------------------------------------

        private FootPlant m_LimbFootL;
        private FootPlant m_LimbFootR;

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => "Align Feet with Ground";
        
        public override string Name => RIG_NAME;
        
        public override bool RequiresHuman => true;

        internal float FootOffset => m_FootOffset;
        internal LayerMask FootMask => this.m_FootMask;

        // IMPLEMENT METHODS: ---------------------------------------------------------------------

        protected override bool DoStartup(Character character)
        {
            this.m_LimbFootL = new FootPlant(HumanBodyBones.LeftFoot,  AvatarIKGoal.LeftFoot,  this, 0);
            this.m_LimbFootR = new FootPlant(HumanBodyBones.RightFoot, AvatarIKGoal.RightFoot, this, 1);

            bool updateGraph = base.DoStartup(character);
            return updateGraph;
        }

        protected override bool DoUpdate(Character character)
        {
            bool updateGraph = base.DoUpdate(character);

            this.m_LimbFootL.Update();
            this.m_LimbFootR.Update();
                        
            return updateGraph;
        }
    }
}