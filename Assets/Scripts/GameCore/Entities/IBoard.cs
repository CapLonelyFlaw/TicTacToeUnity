using System;
using System.Collections.Generic;
using Helpers;

namespace GameCore
{
    public interface IBoard
    {
        ActionWrapper<int, int> OnPiecePlaced { get; }
        List<int> GetOpenSlots();
        int[] GetWinningLine();
        IBoard Clone();
        void PlacePiece(PieceType toPlace, int slot);
        GameStateType DetermineBoardState();
    }
}

