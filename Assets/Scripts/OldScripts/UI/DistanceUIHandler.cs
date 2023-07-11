using TMPro;
using UnityEngine;

public class DistanceUIHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _distanceText;
    private Transform _carTransform;
    private float _startingPosition;
    private float _distanceTraveled;

    private void Start()
    {
        _carTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _startingPosition = _carTransform.position.y;
        _distanceTraveled = 0f;
    }

    private void Update()
    {
        float currentDistance = _carTransform.position.y - _startingPosition;

        _distanceTraveled = Mathf.Max(currentDistance, _distanceTraveled) / 30;
        _distanceText.text = _distanceTraveled.ToString("F0") + "Km";
    }
}