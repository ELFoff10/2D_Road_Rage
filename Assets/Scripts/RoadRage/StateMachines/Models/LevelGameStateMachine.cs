using Enums;
using UniRx;

namespace RoadRage.StateMachines.Models
{
    public interface ILevelGameStateMachine
    {
        GameStateEnum LastGameState { get; }
        IReadOnlyReactiveProperty<GameStateEnum> GameState { get; }
        void SetGameState(GameStateEnum gameStateEnum); 
    }
    
    public class LevelGameStateMachine : ILevelGameStateMachine
    {
        private GameStateEnum _lastGameState = GameStateEnum.None;
        private ReactiveProperty<GameStateEnum> _gameState =
            new ReactiveProperty<GameStateEnum>(GameStateEnum.None);

        public IReadOnlyReactiveProperty<GameStateEnum> GameState => _gameState;
        public GameStateEnum LastGameState => _lastGameState;
        
        public void SetGameState(GameStateEnum gameStateEnum)
        {
            _lastGameState = _gameState.Value;
            _gameState.Value = gameStateEnum;
        }
    }
}