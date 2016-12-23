using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace GameCore
{
    /// <summary>
    /// Simple class which contains the score per slot
    /// </summary>
    public class SlotMark
    {
        public int Mark { get; set; }
        public int Slot { get; set; }
    }

    public class DefaultBoard : IBoard
    {
        #region Fields

        private readonly PieceType[] _slots;
        private readonly int _numberOfSlots;

        private int[] _winningLine;

        #endregion

        #region Properties

        /// <summary>
        /// Occurs when board changed (slot, mark 0-none,1-O,2-X)
        /// </summary>
        public ActionWrapper<int, int> OnPiecePlaced
        {
            get { return _onPiecePlaced; }
        }
        ActionWrapper<int, int> _onPiecePlaced = new ActionWrapper<int, int>();

        public int NumberOfOpenSlots
        {
            get { return _slots.Count(x => x == PieceType.Empty); }
        }
        
        public PieceType[] Slots
        {
            get { return new List<PieceType>(_slots).ToArray(); }
        }

        #endregion

        public DefaultBoard()
        {
            _numberOfSlots = 9;
            _slots = new PieceType[_numberOfSlots];
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i] = PieceType.Empty;
            }
        }

        public GameStateType DetermineBoardState()
        {
            for (int i = 0; i < 4; i++)
            {
                bool isWinningLine = DetermineWinningLine(i);
                if (isWinningLine)
                {
                    return DetermineGameStateType(i, isWinningLine);
                }
            }
            return DetermineGameStateType(-1, false);
        }


        public void PlacePiece(PieceType toPlace, int slot)
        {
            if (!IsValidMove(slot))
            {
                throw new InvalidOperationException(string.Format("Can't place piece at slot '{0}'", slot));
            }
            _slots[slot] = toPlace;
            _onPiecePlaced.Dispatch(slot, (int)toPlace);
        }


        public bool IsValidMove(int slot)
        {
            return ((slot >= 0) && (slot < _numberOfSlots) && (_slots[slot] == PieceType.Empty));
        }


        public IBoard Clone()
        {
            var toReturn = new DefaultBoard();
            Array.Copy(_slots, toReturn._slots, _slots.Length);
            return toReturn;
        }
        
        public int[] GetWinningLine()
        {
            return _winningLine;
        }

        public List<int> GetOpenSlots()
        {
            var toReturn = new List<int>();
            for (int i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == PieceType.Empty)
                {
                    toReturn.Add(i);
                }
            }
            return toReturn;
        }

        private GameStateType DetermineGameStateType(int slot, bool isWin)
        {
            if (isWin)
            {
                return _slots[slot] == PieceType.O ? GameStateType.OWins : GameStateType.XWins;
            }
            // tie or in progress. If there are still slots left, we can't decide yet if it is a tie so report 'in progress'
            return NumberOfOpenSlots > 0 ? GameStateType.InProgress : GameStateType.Tie;
        }


        private bool DetermineWinningLine(int startSlot)
        {
            switch (startSlot)
            {
                case 0:
                    // check vertical
                    if (DetermineIfLineUsingOnePiece(0, 3, 6))
                    {
                        return true;
                    }
                    // check horizontal
                    if (DetermineIfLineUsingOnePiece(0, 1, 2))
                    {
                        return true;
                    }
                    // check diagonal left->right
                    return DetermineIfLineUsingOnePiece(0, 4, 8);
                case 1:
                    // check vertical
                    return DetermineIfLineUsingOnePiece(1, 4, 7);
                case 2:
                    // check vertical
                    if (DetermineIfLineUsingOnePiece(2, 5, 8))
                    {
                        return true;
                    }
                    // check diagonal right->left
                    return DetermineIfLineUsingOnePiece(2, 4, 6);
                case 3:
                    if (DetermineIfLineUsingOnePiece(6, 7, 8))
                    {
                        return true;
                    }
                    // check horizontal.
                    return DetermineIfLineUsingOnePiece(3, 4, 5);
                default:
                    // no need
                    return false;
            }
        }


        private bool DetermineIfLineUsingOnePiece(int piece0, int piece1, int piece2)
        {
            var result = (_slots[piece0] != PieceType.Empty) && (_slots[piece0] == _slots[piece1]) && (_slots[piece0] == _slots[piece2]);
            if (result)
            {
                _winningLine = new[] { piece0, piece1, piece2};
                return true;
            }
            return false;
        }

    }
}

