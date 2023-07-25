using UnityEngine;
using VContainer;

public class DeadlyObstacle : MonoBehaviour
{
    [Inject]
    private readonly ICoreStateMachine _coreStateMachine;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CarController car = collision.transform.root.GetComponent<CarController>();

        if (car != null && car.CompareTag("Player"))
        {
            _coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.RaceOver);
            car.GetComponent<CarInputHandler>().enabled = false;
        }
    }
}