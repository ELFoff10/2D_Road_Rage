using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

public class RaceTimeUIHandler : MonoBehaviour
{
    private TMP_Text _timeText;
    private float _lastRaceTimeUpdate;

    [Inject]
    private readonly ICoreStateMachine _coreStateMachine;

    private CompositeDisposable _compositeDisposable = new CompositeDisposable();

    public int RaceTimer = 0;

    private void Awake()
    {
        _timeText = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        RaceTimer = 0;
        _coreStateMachine.LevelGameStateMachine.GameState.TakeUntilDisable(this).Subscribe(ChangeGameState);
    }

    private void OnDisable()
    {
        _compositeDisposable.Clear();
    }

    #region Subs

    private void ChangeGameState(GameStateEnum gameStateEnum)
    {
        if (gameStateEnum == GameStateEnum.Play)
        {
            RaceTimer = 0;
            Observable.Timer(System.TimeSpan.FromSeconds(1))
                .Repeat()
                .Subscribe(_ =>
                {
                    RaceTimer++;
                    SetLabel(RaceTimer);
                }).AddTo(_compositeDisposable);
        }
    }

    #endregion

    private void SetLabel(int value)
    {
        int raceTimeMinutes = (int)Mathf.Floor(value / 60);
        int raceTimeSeconds = (int)Mathf.Floor(value % 60);

        _timeText.text = $"{raceTimeMinutes.ToString("00")}:{raceTimeSeconds.ToString("00")}";
    }
}