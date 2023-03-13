using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]
    
[Title("Switch Between Game Objects")]
[Description("Switch the Active state Between a Game Objects List")]

[Category("Game Objects/Switch Between Game Objects")]

[Keywords("Set", "Switch", "Between", "GameObject")]
[Image(typeof(IconCubeSolid), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionSwitchBetweenGameObjects : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------

    [SerializeField] private List<GameObject> m_GameObjects;
    [SerializeField] private int m_Index;

    // PROPERTIES: ----------------------------------------------------------------------------

    public override string Title => 
        $"Switch to GameObject";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        if (this.m_GameObjects == null) return DefaultResult;
        for (int i = 0; i < this.m_GameObjects.Count; i++)
        {
            if (i != this.m_Index)
            {
                this.m_GameObjects[i].SetActive(false);
            }
            else if (i == this.m_Index)
            {
                this.m_GameObjects[i].SetActive(true);
            }
        }
        return DefaultResult;
    }
}
