using System;
using UniRx;

public interface ILevelGameStateMachine
{
	GameStateEnum LastGameState { get; }
	IReadOnlyReactiveProperty<GameStateEnum> GameState { get; }
	void SetGameState(GameStateEnum gameStateEnum);

	event Action<GameStateEnum> OnSetGameState;
}

public class LevelGameStateMachine : ILevelGameStateMachine
{
	public event Action<GameStateEnum> OnSetGameState;
	private GameStateEnum _lastGameState = GameStateEnum.None;

	private ReactiveProperty<GameStateEnum> _gameState =
		new ReactiveProperty<GameStateEnum>(GameStateEnum.None);

	public IReadOnlyReactiveProperty<GameStateEnum> GameState => _gameState;
	public GameStateEnum LastGameState => _lastGameState;

	public void SetGameState(GameStateEnum gameStateEnum)
	{
		_lastGameState = _gameState.Value;
		_gameState.Value = gameStateEnum;
		OnSetGameState?.Invoke(gameStateEnum);
	}
}