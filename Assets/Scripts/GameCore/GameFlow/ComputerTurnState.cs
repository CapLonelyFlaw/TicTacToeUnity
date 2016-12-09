

namespace GameCore
{
    public class ComputerTurnState : TurnStateBase
    {

        public ComputerTurnState(GameState gameState)
        {
            _gameState = gameState;
        }

        public override void ChangeTurnState()
        {
            _gameState.ActiveState = _gameState.HumanTurnState;
            base.ChangeTurnState();
        }
    }
}
