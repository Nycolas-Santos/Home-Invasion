using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class GameSettings : Singleton<GameSettings>
    {
        private const VFXMode DEFAULT_VFX_MODE = VFXMode.VHS;
        
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
            var instruction = new InstructionSetVFX();
            var instructionList = new InstructionList(instruction);

            instruction.vfxMode = vfx;
            _ = instructionList.Run(new Args(this.gameObject));
        }
        
        public VFXMode vfx = DEFAULT_VFX_MODE;
        public enum VFXMode
        {
            VHS,
            NoVFX,
        }
    }
}
