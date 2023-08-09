using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class LeaderBoardUIHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject _leaderBoardItemPrefab;
	public Canvas Canvas;

	private SetLeaderBoardItemInfo[] _setLeaderBoardItemInfo;
	private bool _isInitialized;

	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;

	private void OnEnable()
	{
		Canvas = GetComponent<Canvas>();
		Canvas.enabled = false;
	}

	private void Start()
	{
		_coreStateMachine.LevelGameStateMachine.OnSetGameState += CanvasEnable;
		
		var leaderBoardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

		var carLapCounterArray = FindObjectsOfType<CarLapCounter>();

		_setLeaderBoardItemInfo = new SetLeaderBoardItemInfo[carLapCounterArray.Length];

		for (var i = 0; i < carLapCounterArray.Length; i++)
		{
			var leaderBoardInfoGameObject =
				Instantiate(_leaderBoardItemPrefab, leaderBoardLayoutGroup.transform);

			_setLeaderBoardItemInfo[i] = leaderBoardInfoGameObject.GetComponent<SetLeaderBoardItemInfo>();

			_setLeaderBoardItemInfo[i].SetPositionText($"{i + 1}.");
		}

		Canvas.ForceUpdateCanvases();

		_isInitialized = true;
	}

	private void CanvasEnable(GameStateEnum gameStateEnum)
	{
		if (gameStateEnum == GameStateEnum.Finish)
		{
			Canvas.enabled = true;
		}
	}

	private void OnDisable()
	{
		_coreStateMachine.LevelGameStateMachine.OnSetGameState -= CanvasEnable;
	}

	public void UpdateList(List<CarLapCounter> lapCounters)
	{
		if (!_isInitialized)
		{
			return;
		}

		for (var i = 0; i < lapCounters.Count; i++)
		{
			_setLeaderBoardItemInfo[i].SetDriverNameText(lapCounters[i].gameObject.name);
		}
	}
}