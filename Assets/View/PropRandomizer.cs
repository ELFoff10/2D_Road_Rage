using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PropRandomizer : MonoBehaviour
{
	public List<GameObject> PropSpawnPoints;
	public List<GameObject> PropPrefabs;

	private void Start()
	{
		SpawnProps();
	}

	private void SpawnProps()
	{
		foreach (var propSpawnPoint in PropSpawnPoints)
		{
			var random = Random.Range(0, PropPrefabs.Count);
			var prop = Instantiate(PropPrefabs[random], propSpawnPoint.transform.position, quaternion.identity);
			prop.transform.parent = propSpawnPoint.transform;
		}
	}
}