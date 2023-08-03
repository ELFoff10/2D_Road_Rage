using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
	public List<PropRandomizer> TerrainChunks;
	public GameObject Player;
	public float CheckerRadius;
	public LayerMask TerrainMask;
	public GameObject CurrentChunk;
	private Vector3 _playerLastPosition;

	public List<GameObject> SpawnedChunks;
	private GameObject _latestChunk;
	public float MaxOpDist;
	private float _opDist;
	private float _optimizerCooldown;
	public float OptimizerCooldownDur;

	private void Start()
	{
		_playerLastPosition = Player.transform.position;
	}

	private void Update()
	{
		ChunkChecker();
		ChunkOptimizer();
	}

	private string GetDirectionName(Vector3 direction)
	{
		direction = direction.normalized;

		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			switch (direction.y)
			{
				// Moving horizontally more than vertically
				case > 0.5f:
					// Also moving upwards
					return direction.x > 0 ? "Right Up" : "Left Up";
				case < -0.5f:
					// Also moving downwards
					return direction.x > 0 ? "Right Down" : "Left Down";
				default:
					// Moving straight horizontally
					return direction.x > 0 ? "Right" : "Left";
			}
		}
		else
		{
			switch (direction.x)
			{
				// Moving vertically more than horizontally
				case > 0.5f:
					// Also moving right
					return direction.y > 0 ? "Right Up" : "Right Down";
				case < -0.5f:
					// Also moving left
					return direction.y > 0 ? "Left Up" : "Left Down";
				default:
					// Moving straight vertically
					return direction.y > 0 ? "Up" : "Down";
			}
		}
	}

	private void ChunkChecker()
	{
		if (!CurrentChunk)
		{
			return;
		}

		var position = Player.transform.position;
		Vector3 moveDir = position - _playerLastPosition;
		_playerLastPosition = position;

		string directionName = GetDirectionName(moveDir);

		CheckAndSpawnChunk(directionName);

		// Check additional adjacent directions for diagonal chunks
		if (directionName.Contains("Up"))
		{
			CheckAndSpawnChunk("Up");
		}

		if (directionName.Contains("Down"))
		{
			CheckAndSpawnChunk("Down");
		}

		if (directionName.Contains("Right"))
		{
			CheckAndSpawnChunk("Right");
		}

		if (directionName.Contains("Left"))
		{
			CheckAndSpawnChunk("Left");
		}
	}

	private void CheckAndSpawnChunk(string direction)
	{
		if (!Physics2D.OverlapCircle(CurrentChunk.transform.Find(direction).position, CheckerRadius, TerrainMask))
		{
			SpawnChunk(CurrentChunk.transform.Find(direction).position);
		}
	}

	private void SpawnChunk(Vector3 spawnPosition)
	{
		var random = Random.Range(0, TerrainChunks.Count);
		_latestChunk = Instantiate(TerrainChunks[random].gameObject, spawnPosition, Quaternion.identity);
		SpawnedChunks.Add(_latestChunk);
	}

	private void ChunkOptimizer()
	{
		_optimizerCooldown -= Time.deltaTime;

		if (_optimizerCooldown <= 0f)
		{
			_optimizerCooldown = OptimizerCooldownDur;
		}
		else
		{
			return;
		}

		foreach (var chunk in SpawnedChunks)
		{
			_opDist = Vector3.Distance(Player.transform.position, chunk.transform.position);
			if (_opDist > MaxOpDist)
			{
				chunk.SetActive(false);
			}
			else
			{
				chunk.SetActive(true);
			}
		}
	}
}