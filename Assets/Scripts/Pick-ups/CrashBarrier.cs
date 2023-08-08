using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class CrashBarrier : Pickup
{
	[SerializeField]
	private SpriteRenderer _spriteRenderer;
	[SerializeField]
	private List<ParticleSystem> _particles;

	[Inject]
	private readonly GameEventsManager _gameEventsManager;
	[Inject]
	private readonly AudioManager _audioManager;

	private void Awake()
	{
		foreach (var particle in _particles)
		{
			particle.Stop();
		}
	}

	protected override void OnPickedUp(CarController car)
	{
		foreach (var particle in _particles)
		{
			particle.Play();
		}
	}

	protected override void Collect(CarController car)
	{
		IsCollected = true;
		_spriteRenderer.gameObject.SetActive(false);
		_gameEventsManager.PlayerBreakBarrier();
		_audioManager.EventInstances[(int)AudioNameEnum.BarrierCrush].start();
	}
}