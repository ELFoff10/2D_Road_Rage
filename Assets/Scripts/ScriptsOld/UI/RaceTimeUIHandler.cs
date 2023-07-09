using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RaceTimeUIHandler : MonoBehaviour
{
    private Text _timeText;

    private float _lastRaceTimeUpdate;

    private void Awake()
    {
        _timeText = GetComponent<Text>();
    }

    private void Start()
    {
        StartCoroutine(UpdateTimeCO());
    }

    IEnumerator UpdateTimeCO()
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