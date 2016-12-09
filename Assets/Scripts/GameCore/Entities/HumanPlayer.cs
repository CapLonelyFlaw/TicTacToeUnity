using System;
using Helpers;
using UnityEngine;

namespace GameCore
{
    public class HumanPlayer : IPlayer
    {
        #region Properties

        public PieceType PieceType { get; set; }

        public string Title
        {
            get { return GameConfig.Instance.P1Name; }
        }

        public ActionWrapper<SlotMark> OnTurnMade
        {
            get
            {
                _onTurnMade = new ActionWrapper<SlotMark>();
                return _onTurnMade;
            }
        }
        private ActionWrapper<SlotMark> _onTurnMade;

        public int Wins
        {
            get { return _wins; }
            set
            {
                _wins = value;
                PlayerPrefs.SetInt("_wins", value);
                WinsChange.Dispatch(Wins);
            }
        }
        private int _wins;
        public ActionWrapper<int> WinsChange = new ActionWrapper<int>();

        public int Defeats
        {
            get { return _defeats; }
            set
            {
                _defeats = value;
                PlayerPrefs.SetInt("_defeats", value);
                DefeatsChange.Dispatch(Defeats);
            }
        }
        private int _defeats;
        public ActionWrapper<int> DefeatsChange = new ActionWrapper<int>();

        public int Draws
        {
            get { return _draws; }
            set
            {
                _draws = value;
                PlayerPrefs.SetInt("_draws", value);
                DrawnsChange.Dispatch(Draws);
            }
        }
        private int _draws;
        public ActionWrapper<int> DrawnsChange = new ActionWrapper<int>();

        #endregion

        #region Fields

        private ActionWrapper<int> _onSlotActivated;
        private bool _isSlotsObserved = false;

        #endregion

        /// <summary>
        /// Create the instance of human player
        /// </summary>
        /// <param name="onSlotActivated">Action from controll that responsible for user input</param>
        public HumanPlayer(ActionWrapper<int> onSlotActivated)
        {
            _onSlotActivated = onSlotActivated;
            _draws = PlayerPrefs.GetInt("_draws");
            _wins = PlayerPrefs.GetInt("_wins");
            _defeats = PlayerPrefs.GetInt("_defeats");
        }

        public void MakeTurn()
        {
            if (!_isSlotsObserved)
            {
                _onSlotActivated.AddListener(SlotActivated);
                _isSlotsObserved = true;
            }
        }

        private void SlotActivated(int slotIndex)
        {
            _onTurnMade.Dispatch(new SlotMark()
            {
                Slot = slotIndex,
                Mark = (int) PieceType
            });
        }
    }
}