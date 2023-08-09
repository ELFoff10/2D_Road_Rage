using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

public class RaceTimeUIHandler : MonoBehaviour
{
	public int RaceTimer = 0;
	
	private CompositeDisposable _compositeDisposable = new CompositeDisposable();
	private TMP_Text _timeText;

	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;

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
		switch (gameStateEnum)
		{
			case GameStateEnum.Play:
				Observable.Timer(System.TimeSpan.FromSeconds(1))
					.Repeat()
					.Subscribe(_ =>
					{
						RaceTimer++;
						SetLabel(RaceTimer);
					}).AddTo(_compositeDisposable);
				break;
			case GameStateEnum.CountDown:
				_compositeDisposable.Clear();
				SetLabel(RaceTimer);
				break;
		}
	}

	#endregion

	private void SetLabel(int value)
	{
		var raceTimeMinutes = (int)Mathf.Floor(value / 60);
		var raceTimeSeconds = (int)Mathf.Floor(value % 60);

		_timeText.text = $"{raceTimeMinutes.ToString("00")}:{raceTimeSeconds.ToString("00")}";
	}
}