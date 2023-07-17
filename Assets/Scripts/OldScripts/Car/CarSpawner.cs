using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CarSpawner : MonoBehaviour
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

                    if (PlayerPrefs.GetInt($"P{playerNumber}_IsAI") == 1)
                    {
                        foreach (var light2D in car.GetComponentsInChildren<Light2D>())
                        {
                            light2D.enabled = false;
                        }
                        
                        car.GetComponentInChildren<SpriteRenderer>().material = new Material(carData.Material);
                        
                        car.GetComponent<CarSfxHandler>().enabled = false;
                        car.GetComponent<CarInputHandler>().enabled = false;
                        car.name = "AI";
                        car.tag = "AI";
                    }
                    else
                    {
                        car.GetComponent<CarAIHandler>().enabled = false;
                        car.name = "Player";
                        car.tag = "Player";
                        
                        if (_cameraController != null)
                        {
                            _cameraController.SetTarget(car.transform);
                        }
                    }

                    break;
                }
            }
        }
    }
}