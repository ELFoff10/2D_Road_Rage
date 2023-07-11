using System.Collections;
using TMPro;
using UnityEngine;

public class RaceTimeUIHandler : MonoBehaviour
{
    private TMP_Text _timeText;
    private float _lastRaceTimeUpdate;

    private void Awake()
    {
        _timeText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            var raceTime = GameManager.Instance.GetRaceTime();

            if (_lastRaceTimeUpdate != raceTime)
            {
                int raceTimeMinutes = (int)Mathf.Floor(raceTime / 60);
                int raceTimeSeconds = (int)Mathf.Floor(raceTime % 60);

                _timeText.text = $"{raceTimeMinutes.ToString("00")}:{raceTimeSeconds.ToString("00")}";

                _lastRaceTimeUpdate = raceTime;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}