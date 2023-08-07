using FMODUnity;
using UnityEngine;

public class FMOD_Events : MonoBehaviour
{
	
	[field: Header("Game")]
	[field: SerializeField]
	public EventReference Finish { get; private set; }
	[field: SerializeField]
	public EventReference PickUpGem { get; private set; }
	[field: SerializeField]
	public EventReference Firework { get; private set; }
	[field: SerializeField]
	public EventReference BarrierCrush { get; private set; }
	
	[field: Header("Game Background Music")]
	[field: SerializeField]
	public EventReference GameBackgroundMusic { get; private set; }
	[field: SerializeField]
	public EventReference TrainingLevelBgMusic { get; private set; }
	
	[field: Header("UI")]
	[field: SerializeField]
	public EventReference ClickButton { get; private set; }

	[field: SerializeField]
	public EventReference MenuBackgroundMusic { get; private set; }
	
	[field: Header("Car")]
	[field: SerializeField]
	public EventReference CarEngine { get; private set; }

	[field: SerializeField]
	public EventReference CarSkid { get; private set; }

	[field: SerializeField]
	public EventReference CarHit { get; private set; }

}