using TMPro;
using UnityEngine;
using VContainer;

public class DistanceUIHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _distanceText;

    private Transform _carTransform;
    private float _startingPosition;
    private float _distanceTraveled;

    [Inject]
    private readonly ICoreStateMachine _coreStateMachine;
 
    private void Start()
    {
        _carTransform = GameObject.FindGameObjectWithTag("Player").transform;

        _startingPosition = _carTransform.position.y;
        
        _coreStateMachine.LevelGameStateMachine.OnSetGameState += UpdateText;
    }

    private void OnDestroy()
    {
        _coreStateMachine.LevelGameStateMachine.OnSetGameState -= UpdateText;

    }

    private void Update()
    {
        if (_carTransform != null)
        {
            var currentDistance = _carTransform.position.y - _startingPosition;

            _distanceTraveled = (currentDistance <= 0) ? 0 : currentDistance * 10;
            _distanceText.text = (_distanceTraveled > 1000)
                ? (_distanceTraveled / 1000).ToString("F2") + " km"
                : _distanceTraveled.ToString("F0") + " m";
        }
    }

    private void UpdateText(GameStateEnum gameStateEnum)
    {
        if (gameStateEnum == GameStateEnum.CountDown)
        {
            _distanceTraveled = 0;
            _distanceText.text = _distanceTraveled.ToString("F2") + " m";
        }
    }
}