using UnityEngine;

public class WheelParticleHandler : MonoBehaviour
{
	[SerializeField]
	private CarController _carController;
	private ParticleSystem.EmissionModule _particleSystemEmissionModule;
	private ParticleSystem _particleSystemSmoke;
	private float _particleEmissionRate;

	private void Awake()
	{
		_particleSystemSmoke = GetComponent<ParticleSystem>();
		_particleSystemEmissionModule = _particleSystemSmoke.emission;
		_particleSystemEmissionModule.rateOverTime = 0;
	}

	private void Update()
	{
		_particleEmissionRate = Mathf.Lerp(_particleEmissionRate, 0, Time.deltaTime * 5);
		_particleSystemEmissionModule.rateOverTime = _particleEmissionRate;

		if (!_carController.IsTireScreeching(out var lateralVelocity, out var isBraking)) return;

		if (isBraking)
		{
			_particleEmissionRate = 100;
		}
		else
		{
			_particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
		}
	}
}