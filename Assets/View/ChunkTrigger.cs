using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
	[SerializeField]
	private PropRandomizer _targetMap;
	private MapController _mapController;

	private void Start()
	{
		_mapController = FindObjectOfType<MapController>();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			_mapController.CurrentChunk = _targetMap.gameObject;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") == false) return;

		if (_mapController.CurrentChunk == _targetMap.gameObject)
		{
			_mapController.CurrentChunk = null;
		}
	}
}