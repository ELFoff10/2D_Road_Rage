using UnityEngine;
using VContainer;

public class LevelGeneration : MonoBehaviour
{
	[SerializeField]
	private GameObject _roadPrefab;
	[SerializeField]
	private Transform _startRoad;
	
	private Vector3 _lastEndPosition;
	private float _offset;
	private const float PlayerDistanceSpawn = 200f;
	private Transform _carTransform;

	[Inject]
	private readonly PrefabInject _prefabInject;

	private void Awake()
	{
		_lastEndPosition = _startRoad.transform.Find("EndPosition").position;
		SpawnPart();
	}

	private void Start()
	{
		_carTransform = GameObject.FindGameObjectWithTag("Player").transform;
		_carTransform.GetComponent<CarAIHandler>().enabled = false;
	}

	private void Update()
	{
		if (Vector3.Distance(_carTransform.position, _lastEndPosition) < PlayerDistanceSpawn)
		{
			SpawnPart();
		}

		// var time = GameManager.Instance.GetRaceTime();
		//
		// if (time > 0 && (int)time % 5 == 0)
		// {
		// 	_carController.MaxSpeed += 0.001f;
		// }
	}

	private void SpawnPart()
	{
		var part = Instantiate(_roadPrefab, new Vector3(0, _lastEndPosition.y, 0), Quaternion.identity);
		_prefabInject.InjectGameObject(part);
		_lastEndPosition = part.transform.Find("EndPosition").position;
	}
}