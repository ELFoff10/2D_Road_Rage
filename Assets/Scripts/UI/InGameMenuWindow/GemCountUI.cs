using TMPro;
using UnityEngine;
using VContainer;

public class GemCountUI : MonoBehaviour
{
	[SerializeField] 
	private TMP_Text _gemCollectedText;
	
	public int TotalGems;

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
		TotalGems++;
		_gemCollectedText.text = TotalGems.ToString();
	}

	public void UpdateText()
	{
		_gemCollectedText.text = TotalGems.ToString();
	}
}