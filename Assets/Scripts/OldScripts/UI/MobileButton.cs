using UnityEngine;
using UnityEngine.EventSystems;

public class MobileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType
    {
        Left,
        Right
    }

    public ButtonType buttonType;

    private bool _isPressed;
    private float _pressTime;
    public float maxPressDuration = 1f; // Maximum time the button can be held down

    private RectTransform _buttonRectTransform;
    private Vector3 _originalScale;

    private void Start()
    {
        _buttonRectTransform = GetComponent<RectTransform>();
        _originalScale = _buttonRectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        _pressTime = Time.time;

        // Reduce the button's scale when pressed
        _buttonRectTransform.localScale = _originalScale * 0.8f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        _pressTime = 0f;

        // Restore the button's original scale
        _buttonRectTransform.localScale = _originalScale;
    }

    public float GetHorizontalInput()
    {
        if (_isPressed)
        {
            float pressDuration = Time.time - _pressTime;
            float normalizedTime = Mathf.Clamp01(pressDuration / maxPressDuration);
            return (buttonType == ButtonType.Left) ? -normalizedTime : normalizedTime;
        }

        return 0f;
    }
}