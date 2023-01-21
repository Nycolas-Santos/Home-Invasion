using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 0)]

    [Title("Stop Audio Source")]
    [Description(
        "Stops Audio Source"
    )]

    [Category("Audio/Stop Audio Source")]

    [Parameter("Audio Source", "The Audio Source to be stopped")]

    [Keywords("Audio", "Music", "Source", "Background")]
    [Image(typeof(IconHeadset), ColorTheme.Type.Blue)]

    [Serializable]
    public class InstructionCommonAudioSourceStop : Instruction
    {
        [SerializeField] private AudioSource m_AudioSource = null;
        public override string Title => string.Format(
            "Stop Audio Source: {0}",
            this.m_AudioSource != null ? this.m_AudioSource.name : "(none)"
        );

        protected override Task Run(Args args)
        {
            if (!m_AudioSource.isPlaying || m_AudioSource == null) return DefaultResult;
            m_AudioSource.Stop();
            return DefaultResult;
        }
    }
}