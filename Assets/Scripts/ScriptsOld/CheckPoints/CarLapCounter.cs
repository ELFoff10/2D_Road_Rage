using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarLapCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _carPositionText;

    public event Action<CarLapCounter> OnPassCheckPoint;

    private int _passedCheckPointNumber = 0;
    private float _timeAtLastPassedCheckPoint = 0;

    private int _numberOfPassedCheckPoints = 0;

    private int _lapsCompleted = 0;
    private const int LapsToComplete = 1;

    private bool _isRaceCompleted = false;

    private int _carPosition = 0;

    private bool _isHideRoutineRunning = false;
    private float _hideUIDelayTime;


    public void SetCarPosition(int position)
    {
        _carPosition = position;
    }

    public int GetNumberOfCheckPointPassed()
    {
        return _numberOfPassedCheckPoints;
    }

    public float GetTimeAtLastCheckPoint()
    {
        return _timeAtLastPassedCheckPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            if (_isRaceCompleted == true)
            {
                return;
            }

            CheckPoint checkPoint = collision.GetComponent<CheckPoint>();

            if (_passedCheckPointNumber + 1 == checkPoint.CheckPointNumber)
            {
                _passedCheckPointNumber = checkPoint.CheckPointNumber;

                _numberOfPassedCheckPoints++;

                _timeAtLastPassedCheckPoint = Time.time;

                if (checkPoint.IsFinishLine)
                {
                    _passedCheckPointNumber = 0;
                    _lapsCompleted++;

                    if (_lapsCompleted >= LapsToComplete)
                    {
                        _isRaceCompleted = true;
                    }
                }

                OnPassCheckPoint?.Invoke(this);

                if (_isRaceCompleted == true)
                {
                    StartCoroutine(ShowPositionCO(100));

                    if (CompareTag("Player"))
                    {
                        GameManager.Instance.OnRaceCompleted();

                        GetComponent<CarInputHandler>().enabled = false;
                        GetComponent<CarAIHandler>().enabled = true;
                    }
                }
                else if (checkPoint.IsFinishLine)
                {
                    StartCoroutine(ShowPositionCO(1.5f));
                }
            }
        }
    }

    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        _hideUIDelayTime += delayUntilHidePosition;

        _carPositionText.text = _carPosition.ToString();

        _carPositionText.gameObject.SetActive(true);

        if (!_isHideRoutineRunning)
        {
            _isHideRoutineRunning = true;

            yield return new WaitForSeconds(_hideUIDelayTime);

            _carPositionText.gameObject.SetActive(false);

            _isHideRoutineRunning = false;
        }
    }
}