using SpaceShooter;
using UnityEngine;

public class SpawnCars : MonoBehaviour
{
    [SerializeField]
    private CameraController _cameraController;

    private void Start()
    {
        var spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        CarData[] carDatas = Resources.LoadAll<CarData>("CarData/");

        for (var i = 0; i < spawnPoints.Length; i++)
        {
            var spawnPoint = spawnPoints[i].transform;

            var playerSelectedCarID = PlayerPrefs.GetInt($"P{i + 1}SelectedCarID");

            foreach (var carData in carDatas)
            {
                if (carData.CarUniqueID == playerSelectedCarID)
                {
                    var car = Instantiate(carData.CarPrefab, spawnPoint.position, spawnPoint.rotation);

                    var playerNumber = i + 1;

                    //car.GetComponent<CarInputHandler>()._playerNumber = i + 1;

                    if (PlayerPrefs.GetInt($"P{playerNumber}_IsAI") == 1)
                    {
                        car.GetComponent<CarController>().OffSfx();
                        car.GetComponent<CarInputHandler>().enabled = false;
                        car.name = "AI";
                        car.tag = "AI";
                    }
                    else
                    {
                        car.GetComponent<CarAIHandler>().enabled = false;
                        car.name = "Player";
                        car.tag = "Player";
                        _cameraController.SetTarget(car.transform);
                    }

                    break;
                }
            }
        }
    }
}