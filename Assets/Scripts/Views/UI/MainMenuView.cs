using GameCore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private DifficultySelector _difficultySelector;

    public void StartGame()
    {
        GameManager.Instance.Initialize(_difficultySelector.SelectedDifficulty, new DefaultBoard());

        SceneManager.LoadSceneAsync(GameConfig.Instance.GameSceneName);
        SceneManager.sceneLoaded += (arg0, mode) =>
        {
            SceneManager.UnloadScene(GameConfig.Instance.MainSceneName);
        };
    }
}
