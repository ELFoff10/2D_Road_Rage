using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;

public class CarSpawner : MonoBehaviour
{
	[Inject]
	private CameraController _cameraController;

	[SerializeField]
	private List<GameObject> _spawnPoints;
	
	[Inject]
	private readonly PrefabInject _prefabInject;
    
	private void Awake()
	{
		CarData[] carDatas = Resources.LoadAll<CarData>("CarData/");

		for (var i = 0; i < _spawnPoints.Count; i++)
		{
			var spawnPoint = _spawnPoints[i].transform;

			var playerSelectedCarID = PlayerPrefs.GetInt($"P{i + 1}SelectedCarID");

			foreach (var carData in carDatas)
			{
				if (carData.CarUniqueID == playerSelectedCarID)
				{
					var car = Instantiate(carData.CarPrefab, spawnPoint.position, spawnPoint.rotation);

					_prefabInject.InjectGameObject(car);
					
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