using System;
using UnityEngine;

namespace GameCreator.Runtime.Common
{
    [Title("Player with Offset")]
    [Category("Characters/Player with Offset")]
    
    [Image(typeof(IconPlayer), ColorTheme.Type.Green, typeof(OverlayArrowRight))]
    [Description("The position and rotation of the Player plus an offset in local space")]

    [Serializable]
    public class GetLocationCharactersPlayerOffset : PropertyTypeGetLocation
    {
        [SerializeField] private Vector3 m_LocalOffset = Vector3.forward;
        
        [SerializeField] private bool m_Rotate = true;
        
        public override Location Get(Args args)
        {
            return new Location(ShortcutPlayer.Transform, this.m_LocalOffset, this.m_Rotate);
        }

        public override Location Get(GameObject gameObject)
        {
            return new Location(ShortcutPlayer.Transform, this.m_LocalOffset, this.m_Rotate);
        }

        public static PropertyGetLocation Create => new PropertyGetLocation(
            new GetLocationCharactersPlayerOffset()
        );

        public override string String => "Player";
    }
}