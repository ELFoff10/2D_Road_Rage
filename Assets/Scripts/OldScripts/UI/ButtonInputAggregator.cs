using UnityEngine;

public class ButtonInputAggregator : MonoBehaviour
{
    private MobileButton _leftButton;
    private MobileButton _rightButton;

    private void Start()
    {
        if (!_leftButton || _rightButton == null) return;
        _leftButton = GameObject.Find("LeftButton").GetComponent<MobileButton>();
        _rightButton = GameObject.Find("RightButton").GetComponent<MobileButton>();
    }

    public float GetHorizontalInput()
    {
        var horizontalInput = _leftButton.GetHorizontalInput() + _rightButton.GetHorizontalInput();
        return Mathf.Clamp(horizontalInput, -1f, 1f);
    }
}