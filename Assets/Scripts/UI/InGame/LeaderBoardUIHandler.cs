using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class LeaderBoardUIHandler : MonoBehaviour
{
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;

	[SerializeField]
	private GameObject _leaderBoardItemPrefab;

	private SetLeaderBoardItemInfo[] _setLeaderBoardItemInfo;

	private bool _isInitialized;

	public Canvas Canvas;

	private void Awake()
	{
		Canvas = GetComponent<Canvas>();
		Canvas.enabled = false;
	}

	private void Start()
	{
		VerticalLayoutGroup leaderBoardLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();

		CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

		_setLeaderBoardItemInfo = new SetLeaderBoardItemInfo[carLapCounterArray.Length];

		for (int i = 0; i < carLapCounterArray.Length; i++)
		{
			GameObject leaderBoardInfoGameObject =
				Instantiate(_leaderBoardItemPrefab, leaderBoardLayoutGroup.transform);

			_setLeaderBoardItemInfo[i] = leaderBoardInfoGameObject.GetComponent<SetLeaderBoardItemInfo>();

			_setLeaderBoardItemInfo[i].SetPositionText($"{i + 1}.");
		}

		Canvas.ForceUpdateCanvases();

		_isInitialized = true;
	}

	private void OnDisable()
	{
		Canvas.enabled = false;
	}

	// private void Update()
	// {
	// 	OnGameStateChanged();
	// }
	//
	// private void OnGameStateChanged()
	// {
	// 	if (_coreStateMachine.LevelGameStateMachine.GameState.Value == GameStateEnum.RaceOver)
	// 	{
	// 		_canvas.enabled = true;
	// 	}
	// }

	public void UpdateList(List<CarLapCounter> lapCounters)
	{
		if (!_isInitialized)
		{
			return;
		}

		for (int i = 0; i < lapCounters.Count; i++)
		{
			_setLeaderBoardItemInfo[i].SetDriverNameText(lapCounters[i].gameObject.name);
		}
	}
}