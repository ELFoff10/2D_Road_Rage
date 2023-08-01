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
        if (isAppearingOnRightSide)
        {
            _animator.Play("Car UI Appear From Right");
        }
        else
        {
            _animator.Play("Car UI Appear From Left");
        }
    }

    public void StartCarExitAnimation(bool isExitingOnRightSide)
    {
        if (isExitingOnRightSide)
        {
            _animator.Play("Car UI Disappear To Right");
        }
        else
        {
            _animator.Play("Car UI Disappear To Left");
        }
    }

    public void OnCarExitAnimationCompleted()
    {
        Destroy(gameObject);
    }
}