using UnityEngine;
using VContainer;

public class Gem : Pickup
{
	[SerializeField] 
	private SpriteRenderer _spriteRenderer;  
	[SerializeField]
	private ParticleSystem _particleSystem;

	[Inject]
	private readonly GameEventsManager _gameEventsManager;
	[Inject]
	private readonly AudioManager _audioManager;
	
	private void Awake()
	{
		_particleSystem.Stop();
	}

	protected override void OnPickedUp(CarController car)
	{
		_particleSystem.Play();
	}

	protected override void Collect(CarController car)
	{
		IsCollected = true;
		_spriteRenderer.gameObject.SetActive(false);
		_gameEventsManager.GemCollected();
		_audioManager.EventInstances[(int)AudioNameEnum.PickUpGem].start();
	}
}