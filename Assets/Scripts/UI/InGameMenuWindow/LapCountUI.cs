using TMPro;
using UnityEngine;
using VContainer;

public class LapCountUI : MonoBehaviour
{
	[SerializeField] 
	private TMP_Text _lapCountText;
	
	private int _lapCount = 1;

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
		_lapCount = 2;
		_lapCountText.text = _lapCount.ToString();
	}
}