using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 0)]
    
    [Title("Change Material Texture At Index")]
    [Description("Changes the main texture of an instantiated material of a Renderer component")]
    
    [Image(typeof(IconTexture), ColorTheme.Type.Yellow)]

    [Category("Renderer/Change Material Texture At Index")]
    
    [Parameter("Texture", "Texture that replaces the Renderer's instantiated material")]

    [Keywords("Set", "Shader")]
    [Serializable]
    public class InstructionRendererChangeMaterialTextureAtIndex : TInstructionRenderer
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private PropertyGetTexture m_Texture = new PropertyGetTexture();
        [SerializeField] private PropertyGetInteger m_Index = new PropertyGetInteger();

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Change Texture of {this.m_Renderer} to {this.m_Texture}";

        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override Task Run(Args args)
        {
            var index = (int) this.m_Index.Get(args);
            
            GameObject gameObject = this.m_Renderer.Get(args);
            if (gameObject == null) return DefaultResult;

            Renderer renderer = gameObject.Get<Renderer>();
            if (renderer == null || renderer.material == null) return DefaultResult;

            if (renderer.materials[index] == null) return DefaultResult;
            
            renderer.materials[index].mainTexture = this.m_Texture.Get(args);
            return DefaultResult;
        }
    }
}