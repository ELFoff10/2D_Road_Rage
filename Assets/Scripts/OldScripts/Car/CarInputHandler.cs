using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarInputHandler : MonoBehaviour
{
    public bool IsUIInput;

    private CarController _carController;

    private Vector2 _inputVector = Vector2.zero;
    private ButtonInputAggregator _buttonInputAggregator;

    private void Awake()
    {
        _carController = GetComponent<CarController>();
        _buttonInputAggregator = GetComponent<ButtonInputAggregator>();
    }

    private void Update()
    {
        if (IsUIInput)
        {
            if (_buttonInputAggregator != null)
                _inputVector.x = _buttonInputAggregator.GetHorizontalInput();
        }
        else
        {
            _inputVector = Vector2.zero;

            _inputVector.x = Input.GetAxis("Horizontal");
            //inputVector.y = Input.GetAxis("Vertical");
        }

        _carController.SetInputVector(_inputVector);
    }

    public void SetInput(Vector2 newInput)
    {
        _inputVector = newInput;
    }
}