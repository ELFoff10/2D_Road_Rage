using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    [HideInInspector]
    public List<CarLapCounter> CarLapCounters = new List<CarLapCounter>();

    private LeaderBoardUIHandler _leaderBoardUIHandler;

    private void Start()
    {
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();
        CarLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        foreach (CarLapCounter lapCounters in CarLapCounters)
        {
            lapCounters.OnPassCheckPoint += OnPassCheckPoint;
        }

        _leaderBoardUIHandler = FindObjectOfType<LeaderBoardUIHandler>();

        if (_leaderBoardUIHandler != null)
        {
            _leaderBoardUIHandler.UpdateList(CarLapCounters);
        }
    }

    private void OnPassCheckPoint(CarLapCounter carLapCounter)
    {
        // Sort the cars position first based on how many checkpoints they have passed, more is always better. Then sort on time where shorter time os better
        CarLapCounters = CarLapCounters.OrderByDescending(s => s.GetNumberOfCheckPointPassed())
            .ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

        // Get the cars position
        int carPosition = CarLapCounters.IndexOf(carLapCounter) + 1;

        // Tell the lap counter which position the car has
        carLapCounter.SetCarPosition(carPosition);

        if (_leaderBoardUIHandler != null)
        {
            _leaderBoardUIHandler.UpdateList(CarLapCounters);
        }
    }
}