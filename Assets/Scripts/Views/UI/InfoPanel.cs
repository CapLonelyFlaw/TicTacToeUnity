using UnityEngine;
using GameCore;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour 
{
    [SerializeField] private Text _loosesValue;
    [SerializeField] private Text _winsValue;
    [SerializeField] private Text _draftsValue;

    private GameManager Game { get { return GameManager.Instance; } }

    public void Show()
    {
        var myPlayer = Game.MyPlayer as HumanPlayer;
        _loosesValue.text = myPlayer.Defeats.ToString();
        _draftsValue.text = myPlayer.Draws.ToString();
        _winsValue.text = myPlayer.Wins.ToString();

        myPlayer.DefeatsChange.AddListener(i =>
        {
            _loosesValue.text = i.ToString();
        });
        myPlayer.DrawnsChange.AddListener(i =>
        {
            _draftsValue.text = i.ToString();
        });
        myPlayer.WinsChange.AddListener(i =>
        {
            _winsValue.text = i.ToString();
        });
    }
}
