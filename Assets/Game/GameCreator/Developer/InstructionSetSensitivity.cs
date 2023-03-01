using System;
using System.Threading.Tasks;
using Game.Scripts;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Version(1, 0, 0)]

[Title("Set Camera Sensitivity")]
[Category("Custom/Set Camera Sensitivity")]
[Keywords("Camera", "Sensitivity")]

[Image(typeof(IconShotFirstPerson), ColorTheme.Type.Green)]

[Serializable]
public class InstructionSetSensitivity : Instruction
{
    // MEMBERS: -------------------------------------------------------------------------------
    [SerializeField] private GlobalNameVariables m_GlobalNameVariables;
    [SerializeField] private GameSettings.SensitivityMode m_SensitivityMode;
    // PROPERTIES: ----------------------------------------------------------------------------
    public override string Title => $"Set Sensitivity to: {this.m_SensitivityMode}";    
    // RUN METHOD: ----------------------------------------------------------------------------
    protected override Task Run(Args args)
    {
        switch (this.m_SensitivityMode)
        {
            case GameSettings.SensitivityMode.Low:
                this.m_GlobalNameVariables.Set(GameSettings.DEFAULT_SENSITIVITY_FIELD_NAME, GameSettings.DEFAULT_SENSITIVITY_LOW);
                break;
            case GameSettings.SensitivityMode.Default:
                this.m_GlobalNameVariables.Set(GameSettings.DEFAULT_SENSITIVITY_FIELD_NAME, GameSettings.DEFAULT_SENSITIVITY_DEFAULT);
                break;
            case GameSettings.SensitivityMode.High:
                this.m_GlobalNameVariables.Set(GameSettings.DEFAULT_SENSITIVITY_FIELD_NAME, GameSettings.DEFAULT_SENSITIVITY_HIGH);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        GameSettings.Instance.sensitivity = this.m_SensitivityMode;
        GameSettings.Instance.UpdateSensitivityMode();
        return DefaultResult;
    }
}
