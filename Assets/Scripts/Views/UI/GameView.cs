using System;
using UnityEngine;
using GameCore;
using Helpers;

public class GameView : MonoBehaviour 
{
    [SerializeField] private GridView _gridView;
    [SerializeField] private ResultsPopUp _resultsPopUp;
    [SerializeField] private InfoPanel _info;

    private GameManager Game { get { return GameManager.Instance; } }

    void Awake()
    {
        StartGame();
        _info.Show();
    }

    public void Show()
    {
        StartGame();
        _info.Show();
    }

    private void StartGame()
    {
        ActionWrapper<int> clickAbstaction = new ActionWrapper<int>();
        Action<GridCell> cellClick = cell =>
        {
            clickAbstaction.Dispatch(cell.CellIndex);
        };
        _gridView.OnCellClick.AddListener(cellClick);

        Game.GameBoard.OnPiecePlaced.AddListener((slot, mark) =>
        {
            _gridView.SetCellMark(slot, mark);
        });

        Game.Initialize(clickAbstaction);
        Game.Start().AddOnce((winner, winningLine) =>
        {
            if (winner != null)
            {
                _gridView.SelectWinningLine(winningLine);

                var myPlayer = Game.MyPlayer as HumanPlayer;
                _resultsPopUp.Show(winner.Title + " WON!", myPlayer.Wins, myPlayer.Defeats, myPlayer.Draws);
            }
            else
            {
                var myPlayer = Game.MyPlayer as HumanPlayer;
                _resultsPopUp.Show("TIE!", myPlayer.Wins, myPlayer.Defeats, myPlayer.Draws);
            }
        });
    }
}
