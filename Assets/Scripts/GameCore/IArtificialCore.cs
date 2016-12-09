using UnityEngine;

namespace GameCore
{
    public interface IArtificialCore
    {
        SlotMark GetBestSlot(IBoard activeBoard, int depth, PieceType playPiece);
    }
}

