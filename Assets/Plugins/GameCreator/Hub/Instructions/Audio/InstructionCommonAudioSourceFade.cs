using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;


[Version(1, 0, 2)]

[Title("Fade Audio Source")]
[Description("Fades an Audio Source to a target volume over time")]
[Category("Audio/Fade Audio Source")]
[Parameter("Audio Source", "The Audio Source to fade")]
[Parameter("Target Volume", "The final volume after fading")]
[Parameter("Duration", "The length of time in seconds to fade")]
[Keywords("Audio", "Music", "Source", "Background", "Fade")]
[Image(typeof(IconHeadset), ColorTheme.Type.Green)]

[Serializable]
public class InstructionCommonAudioSourceFade : Instruction
{

    // MEMBERS: -------------------------------------------------------------------------------

	[SerializeField] private AudioSource m_AudioSource = null;

	[Range(0f, 1f)]
	[SerializeField] private float m_TargetVolume = 1f;

	[SerializeField] private Transition m_Transition = new Transition();

    // PROPERTIES: ----------------------------------------------------------------------------

	public override string Title => string.Format(
		"Fade {0} to {1} in {2}s",
		this.m_AudioSource != null ? this.m_AudioSource.name : "(none)",
		this.m_TargetVolume,
		this.m_Transition.Duration
	);

    // RUN METHOD: ----------------------------------------------------------------------------

	protected override async Task Run(Args args)
	{
		if (this.m_AudioSource == null)
		{
			Debug.Log("Instruction Fade Audio Source missing audio source");
		}

		float valueSource = this.m_AudioSource.Get<AudioSource>().volume;
		float valueTarget = this.m_TargetVolume;

		ITweenInput tween = new TweenInput<float>(
			valueSource,
			valueTarget,
			this.m_Transition.Duration,
			(a, b, t) => m_AudioSource.Get<AudioSource>().volume = Mathf.Lerp(a, b, t),
			Tween.GetHash(typeof(float), "property:linear-speed"),
			this.m_Transition.EasingType
		);
		
		Tween.To(m_AudioSource.gameObject, tween);

		if (this.m_Transition.WaitToComplete) await this.Until(() => tween.IsFinished);
	}
}