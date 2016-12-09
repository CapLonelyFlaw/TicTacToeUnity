using Helpers;

namespace GameCore
{
    public interface IState
    {
        ActionWrapper<SlotMark> OnTurn { get; }
        IPlayer Player { get; set; }
        bool IsFirst { get; set; }

        void Turn();
        void ResolveOrder();
        void Win();
        void Defeat();
        void Tie();
        void ChangeTurnState();
    }
}

