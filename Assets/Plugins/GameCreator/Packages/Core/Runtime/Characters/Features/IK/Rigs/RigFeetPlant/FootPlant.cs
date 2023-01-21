using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Characters.IK
{
    internal class FootPlant
    {
        private const int RAYCAST_FIXED_SIZE = 10;
        
        private const float RANGE_FEET_UP = 0.2f;
        private const float RANGE_FEET_DOWN = 0.2f;

        private const float MAX_FOOTING_LOCK = 0.25f;
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        [NonSerialized] private readonly RaycastHit[] m_RaycastHitsBuffer = new RaycastHit[RAYCAST_FIXED_SIZE];
        
        [NonSerialized] private Transform m_BoneTransform;
        
        [NonSerialized] private readonly AnimFloat m_WeightPosition = new AnimFloat(0f, 0.5f);
        [NonSerialized] private readonly AnimFloat m_WeightRotation = new AnimFloat(0f, 0.5f);

        [NonSerialized] private bool m_HasHit;
        [NonSerialized] private Vector3 m_HitPoint;
        [NonSerialized] private Vector3 m_HitNormal;

        // PROPERTIES: ----------------------------------------------------------------------------

        [field: NonSerialized] private HumanBodyBones Bone { get; }
        [field: NonSerialized] private AvatarIKGoal AvatarIK { get; }
        
        [field: NonSerialized] private RigFeetPlant Rig { get; }
        [field: NonSerialized] private int Phase { get; }

        private IUnitDriver Driver => this.Rig.Character.Driver;

        private Transform BoneTransform
        {
            get
            {
                if (this.m_BoneTransform == null)
                {
                    Animator animator = this.Rig.Animator;
                    this.m_BoneTransform = animator.GetBoneTransform(this.Bone);
                }

                return this.m_BoneTransform;
            }
        }

        // CONSTRUCTOR: ---------------------------------------------------------------------------
        
        public FootPlant(HumanBodyBones bone, AvatarIKGoal avatarIK, RigFeetPlant rig, int phase)
        {
            this.Bone = bone;
            this.AvatarIK = avatarIK;
            this.Rig = rig;
            this.Phase = phase;

            this.Rig.Character.EventAfterChangeModel += this.RegisterAnimatorIK;
            this.RegisterAnimatorIK();
        }

        private void RegisterAnimatorIK()
        {
            this.Rig.Character.Animim.EventOnAnimatorIK -= this.OnAnimatorIK;
            this.Rig.Character.Animim.EventOnAnimatorIK += this.OnAnimatorIK;
        }
        
        // CALLBACKS: -----------------------------------------------------------------------------

        private void OnAnimatorIK(int layerIndex)
        {
            this.OnAnimatorUpdateFoot();
            this.OnAnimatorSetFoot();
        }

        private void OnAnimatorUpdateFoot()
        {
            Animator animator = this.Rig.Animator;
            if (animator == null) return;
            
            float feetRangeUp = this.Rig.Character.Motion.Height * RANGE_FEET_UP;
            float feetRangeDown = this.Rig.Character.Motion.Height * RANGE_FEET_DOWN;
            
            Vector3 bonePosition = animator.GetIKPosition(this.AvatarIK);
            Quaternion boneRotation = animator.GetIKRotation(this.AvatarIK);
            
            int hitCount = Physics.RaycastNonAlloc(
                new Vector3(
                    bonePosition.x,
                    this.Rig.Character.Feet.y + feetRangeUp,
                    bonePosition.z
                ),
                Vector3.down,
                this.m_RaycastHitsBuffer,
                feetRangeUp + feetRangeDown,
                this.Rig.FootMask,
                QueryTriggerInteraction.Ignore
            );
            
            float minDistance = Mathf.Infinity;
            RaycastHit minHit = new RaycastHit();
            
            for (int i = 0; i < hitCount; ++i)
            {
                RaycastHit hit = this.m_RaycastHitsBuffer[i];
                if (hit.distance > minDistance) continue;

                minHit = hit;
                minDistance = hit.distance;
            }

            if (hitCount > 0)
            {
                this.m_HasHit = true;
                this.m_HitPoint = minHit.point;
                this.m_HitNormal = minHit.normal;
            }
            else
            {
                this.m_HasHit = false;
                this.m_HitPoint = this.BoneTransform.position;
                this.m_HitNormal = Vector3.up;
            }
        }

        private void OnAnimatorSetFoot()
        {
            Animator animator = this.Rig.Animator;
            if (animator == null) return;

            float weight = this.Rig.IsActive && this.Driver.IsGrounded
                ? this.Rig.Character.Phases.Get(this.Phase)
                : 0f;

            if (this.m_HasHit)
            {
                Vector3 rotationAxis = Vector3.Cross(Vector3.up, this.m_HitNormal);
                float angle = Vector3.Angle(Vector3.up, this.m_HitNormal);
                
                Quaternion rotation = Quaternion.AngleAxis(angle * weight, rotationAxis);
                rotation *= animator.GetIKRotation(AvatarIK);

                float offset = this.Rig.FootOffset + this.Driver.SkinWidth;
                Vector3 position = this.m_HitPoint + Vector3.up * offset;

                animator.SetIKPositionWeight(this.AvatarIK, weight);
                animator.SetIKPosition(this.AvatarIK, position);
                
                animator.SetIKRotationWeight(this.AvatarIK, weight);
                animator.SetIKRotation(this.AvatarIK, rotation);
            }
            else
            {
                animator.SetIKPositionWeight(this.AvatarIK, weight);
                animator.SetIKRotationWeight(this.AvatarIK, weight);
            }
        }
        
        // PUBLIC METHODS: ------------------------------------------------------------------------

        public void Update()
        {
            float deltaTime = this.Rig.Character.Time.DeltaTime;

            this.m_WeightPosition.UpdateWithDelta(deltaTime);
            this.m_WeightRotation.UpdateWithDelta(deltaTime);
        }
    }
}