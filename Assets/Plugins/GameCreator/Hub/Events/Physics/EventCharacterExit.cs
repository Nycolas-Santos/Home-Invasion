using System;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Characters;
using UnityEngine;

[Version(1, 0, 0)]

[Title("On Character Exit")]
[Category("Physics/On Character Exit")]
[Description("Executed when a character leaves the Trigger collider")]

[Image(typeof(IconPlayer), ColorTheme.Type.Red)]
[Keywords("Leave", "Through", "Touch", "Collision", "Collide", "Exit")]

[Serializable]
public class EventCharacterExit : GameCreator.Runtime.VisualScripting.Event
{
    public override bool RequiresCollider => true;

    protected override void OnAwake(Trigger trigger)
    {
        base.OnAwake(trigger);
        trigger.RequireRigidbody();
    }
    
    protected override void OnTriggerExit3D(Trigger trigger, Collider collider)
    {
        base.OnTriggerExit3D(trigger, collider);
            
        if (collider.gameObject.GetComponent<Character>() == null) return;
        _ = this.m_Trigger.Execute(collider.gameObject);
    }

    protected override void OnTriggerExit2D(Trigger trigger, Collider2D collider)
    {
        base.OnTriggerExit2D(trigger, collider);
            
        if (collider.gameObject.GetComponent<Character>() == null) return;
        _ = this.m_Trigger.Execute(collider.gameObject);
    }
}