using System;
using System.Linq;
using Helpers;
using JetBrains.Annotations;
using Random = UnityEngine.Random;

namespace GameCore
{
    public class ComputerPlayer : IPlayer
    {
        #region Properties

        public PieceType PieceType { get; set; }

        public string Title
        {
            get { return GameConfig.Instance.P2Name; }
        }

        public ActionWrapper<SlotMark> OnTurnMade
        {
            get { return _onTurnMade; }
        }
        readonly ActionWrapper<SlotMark> _onTurnMade = new ActionWrapper<SlotMark>();

        #endregion

        #region Fields

        private IArtificialCore _artificialCore;
        private IBoard _board;
        private DifficultyType _difficulty;

        #endregion

        public ComputerPlayer([NotNull] IArtificialCore artificialCore, [NotNull] IBoard board,
            DifficultyType difficultyType)
        {
            _artificialCore = artificialCore;
            _board = board;
            _difficulty = difficultyType;
        }

        public void MakeTurn()
        {
            var slot = _artificialCore.GetBestSlot(_board, GameConfig.Instance.GetSearchDepthByDifficulty(_difficulty),
            PieceType);
            slot.Mark = (int)PieceType;
            _onTurnMade.Dispatch(slot);
            
        }
    }
}
