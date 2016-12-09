using Helpers;

namespace GameCore
{
    public interface IPlayer
    {
        PieceType PieceType { get; set; }
        string Title { get; }
        ActionWrapper<SlotMark> OnTurnMade { get; }
        void MakeTurn();
    }
}

