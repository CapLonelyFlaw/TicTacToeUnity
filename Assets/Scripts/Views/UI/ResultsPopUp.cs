using System;
using Assets.Scripts.SharedLib.UI.Animations;
using GameCore;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsPopUp : MonoBehaviour
{
    [SerializeField] private Text _loosesValue;
    [SerializeField] private Text _winsValue;
    [SerializeField] private Text _draftsValue;
    [SerializeField] private Text _winnerLabel;
    [SerializeField] private GameView _gameView;
    [SerializeField] private AnimationPlayer _showPopUp;

    public void Show(string winnerTitle, int wins, int looses, int drafts)
    {
        _winnerLabel.text = (winnerTitle).ToUpper();
        _winsValue.text = wins.ToString();
        _draftsValue.text = drafts.ToString();
        _loosesValue.text = looses.ToString();

        gameObject.SetActive(true);
        _showPopUp.PlayForward();
    }

    public void OnMenuClick()
    {
        SceneManager.LoadSceneAsync(GameConfig.Instance.MainSceneName);
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        SceneManager.UnloadScene(GameConfig.Instance.GameSceneName);
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
    }

    public void OnTryAgainClick()
    {
        GameManager.Instance.Initialize(new DefaultBoard());
        SceneManager.LoadSceneAsync(GameConfig.Instance.GameSceneName);
    }
}
