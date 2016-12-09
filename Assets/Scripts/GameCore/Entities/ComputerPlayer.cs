using System;
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
        private bool _firstStep;

        #endregion

        public ComputerPlayer([NotNull] IArtificialCore artificialCore, [NotNull] IBoard board,
            DifficultyType difficultyType)
        {
            _artificialCore = artificialCore;
            _board = board;
            _difficulty = difficultyType;
            _firstStep = true;
        }

        public void MakeTurn()
        {
            //some hack for first step =(
            var slot = new SlotMark();
            if (_firstStep)
            {
                PlaceToRandomSlot();
            }
            //use mini max for second+ steps
            else
            {
                slot = _artificialCore.GetBestSlot(_board, GameConfig.Instance.GetSearchDepthByDifficulty(_difficulty),
                PieceType);
                slot.Mark = (int)PieceType;
                _onTurnMade.Dispatch(slot);
            }

            
        }

        private void PlaceToRandomSlot()
        {
            var openSlots = _board.GetOpenSlots();
            //place to center if we can
            SlotMark slot;
            if (openSlots.Exists(i => i == 4))
                slot = new SlotMark()
                {
                    Mark = (int)PieceType,
                    Slot = 4,
                };
            else
            {
                //or random turn on border slots
                Random.InitState(DateTime.Now.Millisecond);
                slot = new SlotMark()
                {
                    Mark = (int)PieceType,
                    Slot = openSlots[Random.Range(0, openSlots.Count - 1)],
                };
            }
            _onTurnMade.Dispatch(slot);
            _firstStep = false;
        }
    }
}
