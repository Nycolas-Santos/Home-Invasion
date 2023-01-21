using System;
using System.Threading.Tasks;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;

[Version(1, 0, 2)]

[Title("Store Raycast Position")]
[Description("Stores the Vector3 position of a raycast collision or end point in a variable")]

[Category("Physics 3D/Store Raycast Position")]

[Parameter("Origin", "The Vector3 or game object position of the raycast origin")]
[Parameter("Direction", "The Vector3 direction of the raycast")]
[Parameter("Distance", "The length of the raycast from origin to end point")]
[Parameter("LayerMask", "The LayerMask included in the raycast")]
[Parameter("Store Collision", "The target variable to store the raycast collision position Vector3")]
[Parameter("Store No Collision", "The target variable to store the end position Vector3 of the raycast distance when no collision")]

[Example(
    "The raycast travels from the Origin position in the Direction heading through the LayerMask. If the raycast collides, the position Vector3 is stored in the Store Collision variable. Otherwise the raycast end point position Vector3 at full Distance is stored in the Store No Collision variable."
)]

[Keywords("Ray", "Raycast", "Store", "Position")]
[Image(typeof(IconVector3), ColorTheme.Type.Green)]

[Serializable]
public class InstructionStoreRaycastPosition : Instruction
{

    // MEMBERS: -------------------------------------------------------------------------------

    [SerializeField] private PropertyGetPosition m_Origin = new PropertyGetPosition();
    [SerializeField] private PropertyGetPosition m_Direction = new PropertyGetPosition();
    [SerializeField] private PropertyGetDecimal m_Distance = new PropertyGetDecimal(50f);

    public LayerMask m_LayerMask = -1;

    [SerializeField] private PropertySetVector3 m_StoreCollision;
    [SerializeField] private PropertySetVector3 m_StoreNoCollision;

    // PROPERTIES: ----------------------------------------------------------------------------

    private RaycastHit raycastHit;
    private RaycastHit[] hitBuffer = new RaycastHit[1];

    public override string Title => string.Format(
        "Store Raycast Position to {0} or {1}",
        this.m_StoreCollision, 
        this.m_StoreNoCollision
    );

    // RUN METHOD: ----------------------------------------------------------------------------

    protected override Task Run(Args args)
    {
        Vector3 origin = this.m_Origin.Get(args);
        Vector3 direction = this.m_Direction.Get(args);
        Vector3 destination;
        float distance = (float) this.m_Distance.Get(args);

        if (Physics.Raycast(origin, direction, out raycastHit, distance, m_LayerMask))
        {
// Debug.DrawRay(origin, direction, Color.green, 1.0f, false);

            destination = raycastHit.point;

            m_StoreCollision.Set(destination, args);
        }
        else
        {
// Debug.DrawRay(origin, direction, Color.red, 1.0f, false);

            destination = origin + (direction * distance);

            if (m_StoreNoCollision != null)
            {
                m_StoreNoCollision.Set(destination, args);
            }
        }

        return DefaultResult;
    }
}