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
	public Canvas Canvas;
	
	private SetLeaderBoardItemInfo[] _setLeaderBoardItemInfo;
	private bool _isInitialized;

	private void Awake()
	{
		Canvas = GetComponent<Canvas>();
		Canvas.enabled = false;
	}

	private void Start()
	{
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

	private void OnDisable()
	{
		Canvas.enabled = false;
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