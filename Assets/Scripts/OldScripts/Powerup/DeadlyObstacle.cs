using UnityEngine;

public class DeadlyObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CarController car = collision.transform.root.GetComponent<CarController>();

        if (car != null && car.CompareTag("Player"))
        {
            GameManager.Instance.OnRaceCompleted();

            car.GetComponent<CarInputHandler>().enabled = false;
        }
    }
}