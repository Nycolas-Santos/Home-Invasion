using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Characters;
using UnityEngine;

[Version(1, 0, 0)]

[Title("On Character Enter")]
[Category("Physics/On Character Enter")]
[Description("Executed when a character enters the Trigger collider")]

[Image(typeof(IconPlayer), ColorTheme.Type.Green)]
[Keywords("Pass", "Through", "Touch", "Collision", "Collide", "Enter")]

[Serializable]
public class EventCharacterEnter : GameCreator.Runtime.VisualScripting.Event
{
    public override bool RequiresCollider => true;

    protected override void OnAwake(Trigger trigger)
    {
        base.OnAwake(trigger);
        trigger.RequireRigidbody();
    }
    
    protected override void OnTriggerEnter3D(Trigger trigger, Collider collider)
    {
        base.OnTriggerEnter3D(trigger, collider);
        
        if (collider.gameObject.GetComponent<Character>() == null) return;
        _ = this.m_Trigger.Execute(collider.gameObject);
    }
        
    protected override void OnTriggerEnter2D(Trigger trigger, Collider2D collider)
    {
        base.OnTriggerEnter2D(trigger, collider);
            
        if (collider.gameObject.GetComponent<Character>() == null) return;
        _ = this.m_Trigger.Execute(collider.gameObject);
    }
}
