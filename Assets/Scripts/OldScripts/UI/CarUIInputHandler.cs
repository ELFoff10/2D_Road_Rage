using UnityEngine;

public class CarUIInputHandler : MonoBehaviour
{
    private CarInputHandler _playerCarInputHandler;
    private Vector2 _inputVector = Vector2.zero;

    private void Awake()
    {
        CarInputHandler[] carInputHandlers = FindObjectsOfType<CarInputHandler>();

        foreach (CarInputHandler carInputHandler in carInputHandlers)
        {
            if (carInputHandler.IsUIInput == true)
            {
                _playerCarInputHandler = carInputHandler;
                break;
            }
        }
    }

    public void OnAcceleratePress()
    {
        _inputVector.y = 1.0f;
        _playerCarInputHandler.SetInput(_inputVector);
    }

    public void OnBrakePress()
    {
        _inputVector.y += -1.0f;
        _playerCarInputHandler.SetInput(_inputVector);
    }

    public void OnAccelerateBrakeRelease()
    {
        _inputVector.y = 0.0f;
        _playerCarInputHandler.SetInput(_inputVector);
    }

    public void OnSteerLeftPress()
    {
        _inputVector.x = -1.0f;
        _playerCarInputHandler.SetInput(_inputVector);
    }

    public void OnSteerRightPress()
    {
        _inputVector.x = 1.0f;
        _playerCarInputHandler.SetInput(_inputVector);
    }

    public void OnSteerRelease()
    {
        _inputVector.x = 0.0f;
        _playerCarInputHandler.SetInput(_inputVector);
    }
}