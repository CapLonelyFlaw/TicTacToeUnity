

namespace GameCore
{
    public class HumanTurnState : TurnStateBase
    {
        public override IPlayer Player
        {
            get { return base.Player; }
            set
            {
                _human = value as HumanPlayer;
                base.Player = value;
            }
        }

        private HumanPlayer _human;

        public HumanTurnState(GameState gameState)
        {
            _gameState = gameState;
        }

        public override void ChangeTurnState()
        {
            _gameState.ActiveState = _gameState.ComputerTurnState;
            base.ChangeTurnState();
        }

        public override void Win()
        {
            base.Win();
            _human.Wins++;
        }

        public override void Defeat()
        {
            base.Defeat();
            _human.Defeats++;
        }

        public override void Tie()
        {
            base.Tie();
            _human.Draws++;
        }
    }
}
