using System;
using TMPro;
using UnityEngine;
using VContainer;

public class DistanceUIHandler : MonoBehaviour
{
	[SerializeField]
	private TMP_Text _distanceText;

	private Transform _carTransform;
	private float _startingPosition;
	private float _distanceTraveled;

	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;

	private void Start()
	{
		_carTransform = GameObject.FindGameObjectWithTag("Player").transform;
		_startingPosition = _carTransform.position.y;
	}

	// private void OnEnable()
	// {
	// 	// При изменении _distanceTraveled мы подписываемся? 
	// }

	// private void OnDisable()
	// {
	// 	throw new NotImplementedException();
	// }

	private void Update()
	{
		if (_carTransform != null)
		{
			var currentDistance = _carTransform.position.y - _startingPosition;

			_distanceTraveled = (currentDistance <= 0) ? 0 : currentDistance * 10;
			_distanceText.text = (_distanceTraveled > 1000)
				? (_distanceTraveled / 1000).ToString("F2") + " km"
				: _distanceTraveled.ToString("F0") + " m";
		}
	}
}