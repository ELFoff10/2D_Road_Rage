using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _leaderBoardItemPrefab;

    private SetLeaderBoardItemInfo[] _setLeaderBoardItemInfo;

    private bool _isInitialized = false;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;

        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
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

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager gameManager)
    {
        if (GameManager.Instance.GetGameState() == GameStates.RaceOver)
        {
            _canvas.enabled = true;
        }
    }

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