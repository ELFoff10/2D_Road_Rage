using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using VContainer;

public class AudioManager : MonoBehaviour
{
	[Inject]
	private readonly FMOD_Events _fmodEvents;

	public List<EventInstance> EventInstances;

	private void Awake()
	{
		EventInstances = new List<EventInstance>();
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
		CreateInstance(_fmodEvents.MenuBackgroundMusic);
		CreateInstance(_fmodEvents.GameBackgroundMusic);
		CreateInstance(_fmodEvents.TrainingLevelBgMusic);
		CreateInstance(_fmodEvents.CarEngine);
		CreateInstance(_fmodEvents.CarSkid);
		CreateInstance(_fmodEvents.CarHit);
		CreateInstance(_fmodEvents.Finish);
		CreateInstance(_fmodEvents.PickUpGem);
		CreateInstance(_fmodEvents.Firework);
		CreateInstance(_fmodEvents.BarrierCrush);
	}

	public static void PlayOneShot(EventReference sound)
	{
		RuntimeManager.PlayOneShot(sound);
	}

	private EventInstance CreateInstance(EventReference eventReference)
	{
		var eventInstance = RuntimeManager.CreateInstance(eventReference);
		EventInstances.Add(eventInstance);
		return eventInstance;
	}
}