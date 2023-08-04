using UnityEngine;
using UnityEngine.UI;

public class CarUIHandler : MonoBehaviour
{
	[Header("Car details")]
	public Image CarImage;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponentInChildren<Animator>();
	}

	public void SetupCar(CarData carData)
	{
		CarImage.sprite = carData.CarSelectSprite;
	}

	public void StartCarEntranceAnimation(bool isAppearingOnRightSide)
	{
		_animator.Play(isAppearingOnRightSide ? "Car UI Appear From Right" : "Car UI Appear From Left");
	}

	public void StartCarExitAnimation(bool isExitingOnRightSide)
	{
		_animator.Play(isExitingOnRightSide ? "Car UI Disappear To Right" : "Car UI Disappear To Left");
	}

	public void OnCarExitAnimationCompleted()
	{
		Destroy(gameObject);
	}
}