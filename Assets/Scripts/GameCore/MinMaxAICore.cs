using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore
{
    /// <summary>
    /// Simple MiniMax algorithm that goes through all possible moves and choose the best move, without a tree, but with recursion
    /// </summary>
    public class MinMaxAICore : IArtificialCore
    {
        #region Fields

        private PieceType _playPiece;
        private int _checkDepth;

        #endregion

        /// <summary>
        /// Get best move
        /// </summary>
        /// <param name="activeBoard">the board</param>
        /// <param name="depth">hi depth - hard game</param>
        /// <param name="playPiece">what type of piece ai must use</param>
        /// <returns>best slot for turn</returns>
        public SlotMark GetBestSlot(IBoard activeBoard, int checkDepth, PieceType playPiece)
        {
            _checkDepth = checkDepth;
            _playPiece = playPiece;

            var openSlotsCount = activeBoard.GetOpenSlots().Count;
            if (openSlotsCount == activeBoard.Slots.Count() ||
                openSlotsCount == activeBoard.Slots.Count() - 1)
            {
                return PlaceToRandomSlot(activeBoard, playPiece);
            }
            //use mini max for second+ steps
            return ResolveMiniMax(activeBoard, 0, true);
        }

        private SlotMark PlaceToRandomSlot(IBoard activeBoard, PieceType playPiece)
        {
            var openSlots = activeBoard.GetOpenSlots();

            //place to center if we can
            if (openSlots.Exists(i => i == 4))
                return new SlotMark()
                {
                    Mark = (int)playPiece,
                    Slot = 4,
                };

            //or random turn on corner slots
            Random.InitState(DateTime.Now.Millisecond);
            openSlots = openSlots.Intersect(new int[] { 0, 2, 6, 8 }).ToList();
            return new SlotMark()
            {
                Mark = (int)playPiece,
                Slot = openSlots[Random.Range(0, openSlots.Count - 1)],
            };
        }

        private SlotMark ResolveMiniMax(IBoard activeBoard, int depth, bool aiTurn)
        {
            bool gameEnded = false;
            int scoreCurrentState = GetMiniMaxScore(activeBoard, depth, out gameEnded);
            if (gameEnded)
            {
                return new SlotMark() { Mark = scoreCurrentState };
            }
            var scoresPerOpenSlot = new List<SlotMark>();
            var newDepth = depth + 1;
            // first calculate the scores for the open places still on the board. 
            var pieceToUse = aiTurn ? _playPiece : (_playPiece == PieceType.X ? PieceType.O : PieceType.X);
            foreach (var indexOpenPlaces in activeBoard.GetOpenSlots())
            {
                var activeBoardClone = activeBoard.Clone();
                activeBoardClone.PlacePiece(pieceToUse, indexOpenPlaces);
                var openSlotScore = ResolveMiniMax(activeBoardClone, newDepth, !aiTurn);
                scoresPerOpenSlot.Add(new SlotMark() { Mark = openSlotScore.Mark, Slot = indexOpenPlaces });
            }
            //determine which move is the best. 
            int scoreToSelect = aiTurn ? scoresPerOpenSlot.Max(s => s.Mark) : scoresPerOpenSlot.Min(s => s.Mark);
            return scoresPerOpenSlot.First(s => s.Mark == scoreToSelect);
        }


        /// <summary>
        /// Calculates the minimax score of the board specified, using the depth as weight for the result
        /// </summary>
        /// <param name="activeBoard">The active board.</param>
        /// <param name="depth">The depth of the recursion</param>
        /// <param name="gameEnded">if set to <c>true</c> [game ended].</param>
        /// <returns>
        /// 0 for tie/in-progress games, 10-depth for win, depth-10 for loss.
        /// </returns>
        private int GetMiniMaxScore(IBoard activeBoard, int depth, out bool gameEnded)
        {
            var boardState = activeBoard.DetermineBoardState();
            int toReturn = 0;
            gameEnded = false;
            switch (boardState)
            {
                case GameStateType.OWins:
                    gameEnded = true;
                    toReturn = _playPiece == PieceType.O ? (_checkDepth - depth) : (depth - _checkDepth);
                    break;
                case GameStateType.XWins:
                    gameEnded = true;
                    toReturn = _playPiece == PieceType.X ? (_checkDepth - depth) : (depth - _checkDepth);
                    break;
                case GameStateType.InProgress:
                    toReturn = 0;
                    break;
                case GameStateType.Tie:
                    gameEnded = true;
                    toReturn = 0;
                    break;
            }
            return toReturn;
        }
    }

}

