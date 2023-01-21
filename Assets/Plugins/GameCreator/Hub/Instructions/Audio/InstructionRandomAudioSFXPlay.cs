using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Common.Audio;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting {
    [Version(0, 0, 2)]

    [Title("Play Random Sound Effect")]
    [Description("Plays a random Audio Clip from an array just once")]

    [Category("Audio/Play Random Sound Effect")]

    [Parameter("Audio Clips", "The Audio Clips to be radnomly selected from")]
    [Parameter("Wait To Complete", "Check if you want to wait until the sound finishes")]
    [Parameter("Pitch", "A random pitch value ranging between two values")]
    [Parameter("Transition In", "Time it takes for the sound to fade in")]
    [Parameter("Spatial Blending", "Whether the sound is placed in a 3D space or not")]
    [Parameter("Target", "A Game Object reference that the sound follows as its source")]

    [Keywords("Audio", "Sounds")]
    [Image(typeof(IconMusicNote), ColorTheme.Type.Yellow)]

    [Serializable]
    public class InstructionRandomAudioSFXPlay : Instruction {
        [SerializeField] private AudioClip[] m_AudioClip = null;
        [SerializeField] private bool m_WaitToComplete = false;

        [SerializeField] private AudioConfigSoundEffect m_Config = new AudioConfigSoundEffect();

        public override string Title => string.Format(
            "Play Random SFX of ",
            this.m_AudioClip != null ? this.m_AudioClip.Length : "0"
        );

        protected override async Task Run(Args args) {
            int random = UnityEngine.Random.Range(0, m_AudioClip.Length);

            if (this.m_WaitToComplete) {
                await AudioManager.Instance.SoundEffect.Play(
                    this.m_AudioClip[random],
                    this.m_Config,
                    args
                );
            } else {
                _ = AudioManager.Instance.SoundEffect.Play(
                    this.m_AudioClip[random],
                    this.m_Config,
                    args
                );
            }
        }
    }
}
