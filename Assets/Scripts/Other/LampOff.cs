using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampOff : MonoBehaviour
{
    [SerializeField] private Light2D _lampLight2D;

    [SerializeField] private float _time;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime / _time;

        if ((int)_timer % 2 == 0)
        {
            _lampLight2D.enabled = false;

        }
        if ((int)_timer % 2 == 1)
        {
            _lampLight2D.enabled = true;
        }
    }
}
