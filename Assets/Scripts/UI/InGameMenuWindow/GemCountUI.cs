using TMPro;
using UnityEngine;
using VContainer;

public class GemCountUI : MonoBehaviour
{
	[SerializeField] 
	private TMP_Text _gemCollectedText;
	
	private int _totalGems;

	[Inject]
	private readonly GameEventsManager _gameEventsManager;
	
	private void Start()
	{
		_gameEventsManager.OnGemCollected += OnCoinCollected;
	}

	private void OnDestroy()
	{
		_gameEventsManager.OnGemCollected -= OnCoinCollected;
	}

	private void OnCoinCollected()
	{
		_totalGems++;
		_gemCollectedText.text = _totalGems.ToString();
	}
}