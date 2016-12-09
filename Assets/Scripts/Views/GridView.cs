using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Helpers;

public class GridView : MonoBehaviour
{
    [SerializeField] private List<GridCell> _cells;

    public ActionWrapper<GridCell> OnCellClick = new ActionWrapper<GridCell>();

	// Use this for initialization
	void Awake () 
    {
        foreach (var gridCell in _cells)
	    {
	        gridCell.OnCellClick.AddListener(cell =>
	        {
                OnCellClick.Dispatch(cell);
	        });
	    }
	}

    /// <summary>
    /// set cell mark
    /// </summary>
    /// <param name="cell">cell index</param>
    /// <param name="mark">0 empty, 1 toe, 2 cross</param>
    public void SetCellMark(int cell, int mark)
    {
        var cellView = _cells.FirstOrDefault(gridCell => gridCell.CellIndex == cell);

        if (cellView != null)
        {
            if(mark == 1)
                cellView.SetToe();
            else if(mark == 2)
                cellView.SetCross();
        }
    }

    public void SelectWinningLine(int[] cellsToSelect)
    {
        var cellstToFade = _cells.Select(cell => cell.CellIndex).Except(cellsToSelect);

        foreach (int cellIndex in cellstToFade)
        {
            var cellView = _cells.FirstOrDefault(gridCell => gridCell.CellIndex == cellIndex);

            if (cellView != null)
            {
                cellView.SetHalfAlpha();
            }
        }
    }
}
