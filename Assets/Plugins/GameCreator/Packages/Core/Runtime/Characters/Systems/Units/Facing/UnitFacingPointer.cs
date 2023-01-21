using System;
using UnityEngine;
using GameCreator.Runtime.Common;
using UnityEngine.InputSystem;

namespace GameCreator.Runtime.Characters
{
    [Title("At Pointer")]
    [Image(typeof(IconCursor), ColorTheme.Type.Green)]
    
    [Category("At Pointer")]
    [Description("Rotates towards where the pointer is, relative to the Character")]

    [Serializable]
    public class UnitFacingPointer : TUnitFacing
    {
        private const float MIN_DISTANCE = 0.05f;
        
        private enum Axis
        {
            X,
            Y,
            Z
        }
        
        // EXPOSED MEMBERS: -----------------------------------------------------------------------

        [SerializeField] private Axis m_InPlane;

        // INITIALIZERS: --------------------------------------------------------------------------

        public UnitFacingPointer()
        {
            this.m_InPlane = Axis.Y;
        }

        protected override Vector3 GetDefaultDirection()
        {
            if (!this.Character.IsPlayer || !this.Character.Player.IsControllable)
            {
                return this.DecideDirection(Vector3.zero);
            }
            
            Camera camera = ShortcutMainCamera.Get<Camera>();
            if (camera == null) return this.DecideDirection(Vector3.zero);
            
            Vector2 pointer = Mouse.current?.position.ReadValue() ?? this.Character.Feet;
            Ray ray = camera.ScreenPointToRay(pointer);
            
            Plane ground = new Plane(
                this.m_InPlane switch
                {
                    Axis.X => Vector3.right,
                    Axis.Y => Vector3.up,
                    Axis.Z => Vector3.forward,
                    _ => throw new ArgumentOutOfRangeException()
                },
                this.Character.Feet
            );

            if (!ground.Raycast(ray, out float distance))
            {
                return this.DecideDirection(Vector3.zero);
            }
            
            Vector3 point = ray.GetPoint(distance);
            Vector3 direction = Vector3.Scale(
                point - this.Character.Feet,
                Vector3Plane.NormalUp
            );
                
            Debug.DrawRay(this.Character.Feet, direction, Color.magenta, 1f);

            return this.DecideDirection(direction.sqrMagnitude > MIN_DISTANCE
                ? direction
                : Vector3.zero
            );
        }
        
        // STRING: --------------------------------------------------------------------------------

        public override string ToString() => "At Pointer";
    }
}