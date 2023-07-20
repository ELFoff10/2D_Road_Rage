using Tools.UiManager;
using UnityEngine.SceneManagement;
using VContainer;
using UniRx;

public class InGameMenuUIHandler : UIBehaviour
{
    [Inject] private readonly ICoreStateMachine _coreStateMachine;

    protected override void OnEnable()
    {
        base.OnEnable();
        _coreStateMachine.LevelGameStateMachine.GameState.TakeUntilDisable(this).Subscribe(OnGameStateChanged);
    }

    public void OnRaceAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _coreStateMachine.SetScenesState(ScenesStateEnum.Level1);
    }

    public void OnExitToMainMenu()
    {
        _coreStateMachine.SetScenesState(ScenesStateEnum.Menu);
    }

    private void OnGameStateChanged(GameStateEnum gameStateEnum)
    {
        if (gameStateEnum == GameStateEnum.RaceOver)
        {
            gameObject.SetActive(true);
        }
    }
}