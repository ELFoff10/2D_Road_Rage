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

    private bool isPressed = false;
    private float pressTime = 0f;
    public float maxPressDuration = 1f; // Maximum time the button can be held down

    private RectTransform buttonRectTransform;
    private Vector3 originalScale;

    private void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        originalScale = buttonRectTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        pressTime = Time.time;

        // Reduce the button's scale when pressed
        buttonRectTransform.localScale = originalScale * 0.8f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        pressTime = 0f;

        // Restore the button's original scale
        buttonRectTransform.localScale = originalScale;
    }

    public float GetHorizontalInput()
    {
        if (isPressed)
        {
            float pressDuration = Time.time - pressTime;
            float normalizedTime = Mathf.Clamp01(pressDuration / maxPressDuration);
            return (buttonType == ButtonType.Left) ? -normalizedTime : normalizedTime;
        }

        return 0f;
    }
}