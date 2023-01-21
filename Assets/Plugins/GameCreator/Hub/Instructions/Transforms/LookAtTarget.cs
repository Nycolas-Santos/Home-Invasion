using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]

[Title("Look At Target")]

[Description("Rotate the Source to face a Target")]

[Category("Transforms/Look At Target")]

[Keywords("Rotate", "Rotation", "Look")]

[Image(typeof(IconRotation), ColorTheme.Type.Yellow, typeof(OverlayArrowUp))]

[Serializable]
public class LookAtTarget : Instruction
{
    [SerializeField] private PropertyGetGameObject m_source = new PropertyGetGameObject();
    [SerializeField] private PropertyGetGameObject m_target = new PropertyGetGameObject();
    [SerializeField] private PropertyGetDirection m_worldUp = new PropertyGetDirection(Vector3.up);
    [SerializeField] private bool m_ignoreX = false;
    [SerializeField] private bool m_ignoreY = false;
    [SerializeField] private bool m_ignoreZ = false;
    
    protected override Task Run(Args args)
    {
        GameObject source = m_source.Get(args);
        GameObject target = m_target.Get(args);
        Vector3 worldUp = m_worldUp.Get(args);

        float x = target.transform.position.x;
        float y = target.transform.position.y;
        float z = target.transform.position.z;

        if(m_ignoreX)
            x = source.transform.position.x;
        if(m_ignoreY)
            y = source.transform.position.y;
        if(m_ignoreZ)
            z = source.transform.position.z;

        Vector3 final = new Vector3(x, y, z);
        
        source.transform.LookAt(final, worldUp);
        
        return DefaultResult;
    }
}
