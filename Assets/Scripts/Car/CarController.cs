using System.Collections;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;

	[Header("Car settings")]
	[SerializeField]
	private float _driftFactor = 0.93f;
	[SerializeField]
	private float _accelerationFactor = 5f;
	[SerializeField]
	private float _turnFactor = 3f;
	[SerializeField]
	private float _maxSpeed = 7f;

	private float _withoutPressingAcceleration = 3f;
	private float _accelerationInput = 1;
	private float _defaultMaxSpeed;
	private float _defaultSpeedBeforeAddSpeed;
	private float _defaultSpeedBeforeSlowSpeed;
	private float _steeringInput;
	private float _rotationAngle;
	private float _velocityVsUp;

	private Rigidbody2D _carRigidbody2D;

	private void Awake()
	{
		_carRigidbody2D = GetComponent<Rigidbody2D>();
		_defaultMaxSpeed = _maxSpeed;
	}

	private void FixedUpdate()
	{
		switch (_coreStateMachine.LevelGameStateMachine.GameState.Value)
		{
			case GameStateEnum.None:
			case GameStateEnum.TrainingCheckPoint1:
			case GameStateEnum.CountDown:
				return;
		}

		ApplyEngineForce();
		KillOrthogonalVelocity();
		ApplySteering();
	}

	private void ApplyEngineForce()
	{
		// Calculate how much "forward" we are going in terms of the direction of our velocity
		_velocityVsUp = Vector2.Dot(transform.up, _carRigidbody2D.velocity);

		// Limit so we cannot go faster than the max speed in the "forward: direction
		if (_velocityVsUp > _maxSpeed && _accelerationInput > 0) return;

		// Limit so we cannot go faster than the 25% of max speed in the "reverse" direction
		// if (_velocityVsUp < _maxSpeed * 0.25f && _accelerationInput < 0) return;

		// Limit so we cannot go faster in any direction while accelerating
		if (_carRigidbody2D.velocity.sqrMagnitude > _maxSpeed * _maxSpeed && _accelerationInput > 0) return;

		// Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
		if (_accelerationInput == 0)
		{
			_carRigidbody2D.drag = Mathf.Lerp(_carRigidbody2D.drag, _withoutPressingAcceleration, Time.fixedDeltaTime * _withoutPressingAcceleration);
		}
		else
		{
			_carRigidbody2D.drag = 0;
		}

		Vector2 engineForceVector = transform.up * (_accelerationInput * _accelerationFactor);
		_carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
	}

	private void ApplySteering()
	{
		// Limit the cars ability to turn when moving slowly
		var minSpeedBeforeAllowTurningFactor = (_carRigidbody2D.velocity.magnitude / 8);
		minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

		_rotationAngle -= _steeringInput * _turnFactor * minSpeedBeforeAllowTurningFactor;
		_carRigidbody2D.MoveRotation(_rotationAngle);
	}

	private void KillOrthogonalVelocity()
	{
		Vector2 forwardVelocity = transform.up * Vector2.Dot(_carRigidbody2D.velocity, transform.up);
		Vector2 rightVelocity = transform.right * Vector2.Dot(_carRigidbody2D.velocity, transform.right);

		_carRigidbody2D.velocity = forwardVelocity + rightVelocity * _driftFactor;
	}

	private float GetLateralVelocity()
	{
		return Vector2.Dot(transform.right, _carRigidbody2D.velocity);
	}

	public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking)
	{
		lateralVelocity = GetLateralVelocity();
		isBreaking = false;

		if (_accelerationInput < 0 && _velocityVsUp > 0)
		{
			isBreaking = true;
			return true;
		}

		if (Mathf.Abs(GetLateralVelocity()) > 1.0f)
		{
			return true;
		}

		return false;
	}

	public void SetInputVector(Vector2 inputVector)
	{
		_steeringInput = inputVector.x;
		//_accelerationInput = inputVector.y;
	}

	public void AddSpeed(float speed)
	{
		_maxSpeed = _defaultMaxSpeed + speed;
		StartCoroutine(AddSpeedCoroutine());
	}

	public void SlowSpeed(float speed)
	{
		_maxSpeed = _defaultMaxSpeed - speed;
		StartCoroutine(SlowSpeedCoroutine());
	}

	private IEnumerator AddSpeedCoroutine()
	{
		yield return new WaitForSeconds(3f);
		_maxSpeed = _defaultMaxSpeed;
	}

	private IEnumerator SlowSpeedCoroutine()
	{
		yield return new WaitForSeconds(3f);
		_maxSpeed = _defaultMaxSpeed;
	}

	internal float GetVelocityMagnitude()
	{
		return _carRigidbody2D.velocity.magnitude;
	}
}