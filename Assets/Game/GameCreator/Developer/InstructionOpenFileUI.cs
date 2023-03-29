using System;
using System.Threading.Tasks;
using Game.Scripts.Phone;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(1, 0, 0)]
    
[Title("Open File UI")]
[Description("Open the File UI")]

[Category("Custom/Open File UI")]

[Keywords("File", "UI", "Open")]
[Image(typeof(IconPaste), ColorTheme.Type.Yellow)]

[Serializable]
public class InstructionOpenFileUI : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private bool m_WaitToClose;

    [SerializeField] private FileScriptableObject file;
    // PROPERTIES: ----------------------------------------------------------------------------
    
    public override string Title => 
        $"Open File UI and wait == {this.m_WaitToClose}";

    // RUN METHOD: ----------------------------------------------------------------------------
    protected override async Task Run(Args args)
    {
        var file = File.Instance;
        if (file == null) return;
        
        file.OpenUI(this.file);
        await this.While(() => this.m_WaitToClose && file.IsOpen());
    }
}
