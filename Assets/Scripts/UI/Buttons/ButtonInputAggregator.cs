using UnityEngine;

public class ButtonInputAggregator : MonoBehaviour
{
    private MobileButton _leftButton;
    private MobileButton _rightButton;

    private void Start()
    {
        _leftButton = GameObject.Find("LeftButton")?.GetComponent<MobileButton>();
        _rightButton = GameObject.Find("RightButton")?.GetComponent<MobileButton>();
    }

    public float GetHorizontalInput()
    {
        if (!_leftButton && _rightButton) return 0;
        var horizontalInput = _leftButton.GetHorizontalInput() + _rightButton.GetHorizontalInput();
        return Mathf.Clamp(horizontalInput, -1f, 1f);
    }
}