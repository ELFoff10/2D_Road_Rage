using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    public bool IsUIInput = false;
    private CarController _carController;
    private ButtonInputAggregator _buttonInputAggregator;
    private Vector2 _inputVector = Vector2.zero;
    

    private void Awake()
    {
        _carController = GetComponent<CarController>();
        _buttonInputAggregator = GetComponent<ButtonInputAggregator>();
    }

    private void Update()
    {
        if (IsUIInput == true)
        {
            if(_buttonInputAggregator != null)
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