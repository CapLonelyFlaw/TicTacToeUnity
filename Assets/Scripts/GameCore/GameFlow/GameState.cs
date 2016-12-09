using Helpers;

namespace GameCore
{
    public class GameState : IState
    {
        #region Properties

        public IState ActiveState { get; set; }
        public IState HumanTurnState { get; set; }
        public IState ComputerTurnState { get; set; }

        public ActionWrapper<SlotMark> OnTurn { get { return ActiveState.OnTurn; } }
        public ActionWrapper<IPlayer, int[]> OnGameEnd = new ActionWrapper<IPlayer, int[]>();
        public bool IsFirst { get { return ActiveState.IsFirst; } set { ActiveState.IsFirst = value; } }

        public IPlayer Player
        {
            get { return ActiveState.Player; }
            set { ActiveState.Player = value; }
        }

        private GameManager Game { get { return GameManager.Instance; } }

        #endregion

        #region Fields

        private GameStateType _lastGameStateType;

        #endregion

        public void ResolveOrder()
        {
            //human turn firs by default
            if (ActiveState == null)
                ActiveState = HumanTurnState;

            ActiveState.ResolveOrder();
            NotActiveState().ResolveOrder();

            ActiveState.Player.PieceType = PieceType.X;
            ActiveState.IsFirst = true;
            NotActiveState().Player.PieceType = PieceType.O;
        }

        public void Turn()
        {
            ObserveState();
            ActiveState.Turn();
        }

        private void ObserveState()
        {
            ActiveState.OnTurn.AddOnce(mark =>
            {
                Game.GameBoard.PlacePiece(ActiveState.Player.PieceType, mark.Slot);
                _lastGameStateType = Game.GameBoard.DetermineBoardState();
                if (_lastGameStateType == GameStateType.InProgress)
                {
                    ChangeTurnState();
                    Turn();
                    return;
                }
                if (_lastGameStateType == GameStateType.Tie)
                {
                    Tie();
                    OnGameEnd.Dispatch(null, null);
                }
                else
                {
                    Win();
                    OnGameEnd.Dispatch(ActiveState.Player, Game.GameBoard.GetWinningLine());
                }

            });
        }

        public void ChangeTurnState()
        {
            ActiveState.ChangeTurnState();
        }

        public void Win()
        {
            ActiveState.Win();
            NotActiveState().Defeat();
        }

        public void Tie()
        {
            HumanTurnState.Tie();
            ComputerTurnState.Tie();
        }

        public void Defeat()
        {
            ActiveState.Defeat();
            NotActiveState().Win();
        }

        private IState NotActiveState()
        {
            return ActiveState == ComputerTurnState ? HumanTurnState : ComputerTurnState;
        }
    }
}