using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;


[Version(1, 0, 1)]
[Title("Set Quality Level")]
[Description("Sets the Quality Presets")]
[Category("US Studios/Settings/Quality Settings")]   
[Parameter("Usage", "Reads Quality Level by Index, and sets a Level by Number")]
[Parameter("Note:", "Index begins with 0 (first in List)")]
[Example("Sets a Quality Level (Project Settings/Quality) & Shows your List directly inside the Instruction")]
[Keywords("Quality", "Mode", "Preset", "UI", "US Studios")]
[Image(typeof(IconUnity), ColorTheme.Type.White)]


[Serializable]
public class InstructionQualityLevel : Instruction
{
	[SerializeField] private string[] m_QualityList;
	[SerializeField] private int m_QualityLevel;
	public override string Title => $"Set Quality Level";

    
    protected override Task Run(Args args)
	{
		this.m_QualityList = QualitySettings.names;
		QualitySettings.SetQualityLevel(m_QualityLevel, true);
		return DefaultResult;
    }
}
