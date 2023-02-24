using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using GameCreator.Runtime.VisualScripting;


[Version(1, 0, 1)]

[Title("Set Selected UI GameObject")]
[Description("Sets a game object as selected in the Event System")]

[Category("UI/Set Selected UI GameObject")]

[Parameter("Game Object", "The target game object to set as selected")]

[Example(
	"Registers a UI game object, e.g. a button, as the currently selected UI item in the Event System. This requires an Event System component to exist in scene."
)]

[Keywords("UI", "Selected", "First Selected","Event System", "Set")]
[Image(typeof(IconButton), ColorTheme.Type.TextLight)]

[Serializable]
public class InstructionUISetSelectedGameObject : Instruction
{
	// MEMBERS: -------------------------------------------------------------------------------

	[SerializeField] private PropertyGetGameObject m_GameObject = new PropertyGetGameObject();

	// PROPERTIES: ----------------------------------------------------------------------------

	public override string Title => string.Format(
		"Set Selected UI GO to {0}",
		this.m_GameObject
	);

	// RUN METHOD: ----------------------------------------------------------------------------

	protected override Task Run(Args args)
	{
			if ( EventSystem.current == null )
			{
				Debug.Log("Set Selected UI GameObject missing GameObject");
			}

		GameObject gameObject = this.m_GameObject.Get(args);

		if (gameObject == null) return DefaultResult;

		EventSystem.current.SetSelectedGameObject(gameObject);

		return DefaultResult;
	}
}