using TMPro;
using UniRx;
using UnityEngine;
using VContainer;

public class CountDownUIHandler : MonoBehaviour
{
	[Inject]
	private readonly ICoreStateMachine _coreStateMachine;

	[Inject]
	private readonly ITimerService _timerService;

	[SerializeField]
	private TMP_Text _countDownText;

	private CompositeDisposable _disposable = new CompositeDisposable();

	private int _countDownTimer;

	private void OnEnable()
	{
		_countDownTimer = 3;
		_coreStateMachine.LevelGameStateMachine.GameState.TakeUntilDisable(this).Subscribe(ChangeGameState);
	}

	private void OnDisable()
	{
		_disposable.Clear();
	}

	private void ChangeGameState(GameStateEnum gameStateEnum)
	{
		if (gameStateEnum == GameStateEnum.CountDown)
		{
			_countDownTimer = 3;
			_countDownText.text = _countDownTimer.ToString();
			Observable.Timer(System.TimeSpan.FromSeconds(1))
				.Repeat()
				.Subscribe(_ =>
				{
					_countDownTimer--;
					_countDownText.text = _countDownTimer.ToString();

					switch (_countDownTimer)
					{
						case 0:
							_countDownText.text = "GO!";
							break;
						case < 0:
							_coreStateMachine.LevelGameStateMachine.SetGameState(GameStateEnum.Play);
							_disposable.Clear();
							gameObject.SetActive(false);
							break;
					}
				}).AddTo(_disposable);
		}
	}
}