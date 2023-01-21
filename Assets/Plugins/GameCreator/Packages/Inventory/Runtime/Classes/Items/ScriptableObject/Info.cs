using System;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.Inventory
{
    [Serializable]
    public class Info
    {
        [SerializeField] private PropertyGetString m_Name = GetStringString.Create;
        [SerializeField] private PropertyGetString m_Description = GetStringTextArea.Create();
        
        [SerializeField] private PropertyGetSprite m_Sprite = GetSpriteInstance.Create();
        [SerializeField] private PropertyGetColor m_Color = GetColorColorsWhite.Create;
        
        // METHODS: -------------------------------------------------------------------------------

        public string Name(Args args) => this.m_Name.Get(args);
        public string Description(Args args) => this.m_Description.Get(args);
        
        public Sprite Sprite(Args args) => this.m_Sprite.Get(args);
        public Color Color(Args args) => this.m_Color.Get(args);
    }
}