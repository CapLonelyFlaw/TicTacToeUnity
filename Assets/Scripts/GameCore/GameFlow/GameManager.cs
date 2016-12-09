using Helpers;

namespace GameCore
{
    public class GameManager : NonMonoSingleton<GameManager>
    {
        #region Properties
        
        public IPlayer MyPlayer { get; private set; }
        public IBoard GameBoard { get; private set; }

        #endregion

        #region Fields

        private GameState _gameState;
        private DifficultyType _difficulty;

        #endregion

        public GameManager()
        {
            GameBoard = new DefaultBoard();
            _gameState = new GameState();
            _gameState.ComputerTurnState = new ComputerTurnState(_gameState);
            _gameState.HumanTurnState = new HumanTurnState(_gameState);
        }
        
        public void Initialize(IBoard gameBoard)
        {
            GameBoard = gameBoard;
        }

        public void Initialize(DifficultyType difficulty, IBoard gameBoard)
        {
            _difficulty = difficulty;
            GameBoard = gameBoard;
        }

        /// <summary>
        /// Initialize players and pass input event
        /// </summary>
        /// <param name="onSlotActivated"></param>
        public void Initialize(ActionWrapper<int> onSlotActivated)
        {
            MyPlayer = new HumanPlayer(onSlotActivated);
            var computerPlayer = new ComputerPlayer(new MinMaxAICore(), GameBoard, _difficulty);

            _gameState.ComputerTurnState.Player = computerPlayer;
            _gameState.HumanTurnState.Player = MyPlayer;
        }

        /// <summary>
        /// Start game logic
        /// </summary>
        /// <returns>an event that says when the game ends, and contains a link to the winner and winner line indexes</returns>
        public ActionWrapper<IPlayer, int[]> Start()
        {
            _gameState.ResolveOrder();
            _gameState.Turn();

            return _gameState.OnGameEnd;
        }
    }
}

