using Helpers;

namespace GameCore
{
    public class TurnStateBase : IState
    {
        #region Properties

        public ActionWrapper<SlotMark> OnTurn { get { return Player.OnTurnMade; } }
        public virtual IPlayer Player { get; set; }
        public bool IsFirst { get; set; }

        #endregion

        #region Fields

        protected GameState _gameState;
        protected PlayerStateType _playerState;

        #endregion

        public virtual void ResolveOrder()
        {
            //if player loose he change turn state
            if (_playerState == PlayerStateType.Loose)
                ChangeTurnState();
            //if tie, turn goes to the player who turns second
            if (_playerState == PlayerStateType.Tie && IsFirst)
                ChangeTurnState();
        }

        public virtual void Turn()
        {
            Player.MakeTurn();
        }

        public virtual void Win()
        {
            _playerState = PlayerStateType.Win;
        }

        public virtual void Defeat()
        {
            _playerState = PlayerStateType.Loose;
        }

        public virtual void Tie()
        {
            _playerState = PlayerStateType.Tie;
        }

        public virtual void ChangeTurnState()
        {

        }
    }
}