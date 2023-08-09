using TMPro;
using UnityEngine;
using VContainer;

public class LapCountUI : MonoBehaviour
{
	[SerializeField] 
	private TMP_Text _lapCountText;
	
	public int LapCount = 1;

	[Inject]
	private readonly GameEventsManager _gameEventsManager;
	
	private void Start()
	{
		_gameEventsManager.OnFinishLinePassed += OnFinishPassed;
	}

	private void OnDestroy()
	{
		_gameEventsManager.OnFinishLinePassed -= OnFinishPassed;
	}

	private void OnFinishPassed()
	{
		LapCount = 2;
		_lapCountText.text = LapCount.ToString();
	}

	public void UpdateText()
	{
		_lapCountText.text = LapCount.ToString();
	}
}
