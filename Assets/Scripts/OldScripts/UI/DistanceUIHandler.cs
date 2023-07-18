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
    }

    private void Update()
    {
        float currentDistance = _carTransform.position.y - _startingPosition;

        _distanceTraveled = (currentDistance <= 0) ? 0: currentDistance * 10;
        _distanceText.text = (_distanceTraveled > 1000) ? (_distanceTraveled / 1000).ToString("F2") + "km" : _distanceTraveled.ToString("F0") + "m";
    }
}