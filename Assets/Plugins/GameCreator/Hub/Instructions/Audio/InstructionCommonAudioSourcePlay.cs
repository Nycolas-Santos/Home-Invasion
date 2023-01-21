using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(1, 0, 0)]

    [Title("Play Audio Source")]
    [Description(
        "Plays Audio Source"
    )]

    [Category("Audio/Play Audio Source")]

    [Parameter("Audio Source", "The Audio Source to be played")]

    [Keywords("Audio", "Music", "Source", "Background")]
    [Image(typeof(IconHeadset), ColorTheme.Type.Yellow)]

    [Serializable]
    public class InstructionCommonAudioSourcePlay : Instruction
    {
        [SerializeField] private AudioSource m_AudioSource = null;
        public override string Title => string.Format(
            "Play Audio Source: {0}",
            this.m_AudioSource != null ? this.m_AudioSource.name : "(none)"
        );

        protected override Task Run(Args args)
        {
            if (m_AudioSource.isPlaying || m_AudioSource == null) return DefaultResult;
            m_AudioSource.Play();
            return DefaultResult;
        }
    }
}