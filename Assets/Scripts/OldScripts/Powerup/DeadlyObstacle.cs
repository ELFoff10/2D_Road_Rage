using UnityEngine;
using VContainer;

public class DeadlyObstacle : MonoBehaviour
{
    // [Inject]
    // private TestTimer _testTimer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CarController car = collision.transform.root.GetComponent<CarController>();

        if (car != null && car.CompareTag("Player"))
        {
            // _testTimer.OnRaceCompleted();
            
            //GameManager.Instance.OnRaceCompleted();

            car.GetComponent<CarInputHandler>().enabled = false;
        }
    }
}