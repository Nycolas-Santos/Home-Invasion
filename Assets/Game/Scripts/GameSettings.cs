using System;
using GameCreator.Runtime.Cameras;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Bloom = UnityEngine.Rendering.Universal.Bloom;
using ChromaticAberration = UnityEngine.Rendering.Universal.ChromaticAberration;
using LoadSceneMode = UnityEngine.SceneManagement.LoadSceneMode;

namespace Game.Scripts
{
    public class GameSettings : Singleton<GameSettings>
    {
        private const VFXMode DEFAULT_VFX_MODE = VFXMode.VHS;
        private const SensitivityMode DEFAULT_SENSITIVITY_MODE = SensitivityMode.Default;
        
        public const string DEFAULT_SENSITIVITY_FIELD_NAME = "Sensitivity";

        private const int LOW_QUALITY_INDEX = 0;
        private const int MEDIUM_QUALITY_INDEX = 1;
        private const int HIGH_QUALITY_INDEX = 2;
        
        public static readonly Vector3 DEFAULT_SENSITIVITY_HIGH = new Vector3(75,75,0);
        public static readonly Vector3 DEFAULT_SENSITIVITY_DEFAULT = new Vector3(50,50,0);
        public static readonly Vector3 DEFAULT_SENSITIVITY_LOW = new Vector3(25,25,0);


        public VFXMode vfx = DEFAULT_VFX_MODE;
        public SensitivityMode sensitivity = SensitivityMode.Default;
        public enum VFXMode
        {
            VHS,
            NoVFX,
        }

        public enum SensitivityMode
        {
            Low,
            Default,
            High
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void OnSubystemInit()
        {
            Instance.WakeUp();
        }
        
        // PRIVATE PROPERTIES: --------------------------------------------------------------------

        protected override bool SurviveSceneLoads => true;
        
        // PUBLIC PROPERTIES: ---------------------------------------------------------------------
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneChange;
        }
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneChange;
        }
        private void OnSceneChange(Scene arg0, LoadSceneMode arg1)
        {
            UpdateVFXMode();
            UpdateSensitivityMode();
            UpdateQualityMode();
        }

        public void UpdateQualityMode()
        {
            var volume = Camera.main.gameObject.GetComponent<Volume>();
            var camera = Camera.main.gameObject.GetComponent<UniversalAdditionalCameraData>();

            if (volume == null) return;
            if (camera == null) return;

            switch (QualitySettings.GetQualityLevel())
            {
                case 0: // LOW QUALITY
                    camera.antialiasing = AntialiasingMode.None;
                    break;
                case 1: // MEDIUM QUALITY
                    camera.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                    break;
                case 2: // HIGH QUALITY
                    camera.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
                    break;
            }
        }

        public void UpdateVFXMode()
        {
            var instruction = new InstructionSetVFX();
            var instructionList = new InstructionList(instruction);

            instruction.m_VfxMode = vfx;
            _ = instructionList.Run(new Args(this.gameObject));
        }

        public void UpdateSensitivityMode()
        {
            var cameraShot = ShortcutMainShot.Get<ShotCamera>();
            if (cameraShot == null) return;
            var type = cameraShot.ShotType as ShotTypeFirstPerson;
            if (type == null || type.GetType() != typeof(ShotTypeFirstPerson)) return;
            var system = type.GetSystem(ShotSystemFirstPerson.ID) as ShotSystemFirstPerson;
            if (system == null || system.GetType() != typeof(ShotSystemFirstPerson)) return;
            
            switch (sensitivity)
            {
                case SensitivityMode.Low:
                    system.Sensitivity = DEFAULT_SENSITIVITY_LOW;
                    break;
                case SensitivityMode.Default:
                    system.Sensitivity = DEFAULT_SENSITIVITY_DEFAULT;
                    break;
                case SensitivityMode.High:
                    system.Sensitivity = DEFAULT_SENSITIVITY_HIGH;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
