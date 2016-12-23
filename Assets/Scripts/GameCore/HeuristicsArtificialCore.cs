using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class HeuristicsArtificialCore : IArtificialCore
    {
        public SlotMark GetBestSlot(IBoard activeBoard, int depth, PieceType playPiece)
        {
            Debug.Log("Heuristics step");
            var slots = activeBoard.Slots;
            var opponentPiece = playPiece == PieceType.X ? PieceType.O : PieceType.X;

            var openSlots = activeBoard.GetOpenSlots();
            if (openSlots.Exists(i => i == 4))
                return new SlotMark()
                {
                    Mark = (int)playPiece,
                    Slot = 4,
                };
            
            int[,] rows = new int[8, 3]
            {
                {0, 1, 2},
                {0, 4, 8},
                {1, 4, 7},
                {2, 5, 8},
                {2, 4, 6},
                {6, 7, 8},
                {3, 4, 5},
                {0, 3, 6}
            };

            for (int i = 0; i < 8; i++)
            {
                if (IfTwoInRow(rows[i, 0], rows[i, 1], rows[i, 2], opponentPiece, slots))
                {
                    return new SlotMark()
                    {
                        Mark = (int)playPiece,
                        Slot = GetEmpty(rows[i, 0], rows[i, 1], rows[i, 2], slots)
                    };
                }
            }

            int[] _slotsToCheck = new int[] {0, 6, 8, 2, 3, 5, 1, 7, 4};

            List<int> nearOpponentsBySlot = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[]{3, 4, 1}, 0, opponentPiece, slots) + 1);
                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[] { 3, 4, 7 }, 6, opponentPiece, slots) + 1);
                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[] { 7, 4, 5 }, 8, opponentPiece, slots) + 1);
                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[] { 1, 4, 5 }, 2, opponentPiece, slots) + 1);

                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[] { 6, 7, 4, 1, 0 }, 3, opponentPiece, slots));
                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[] { 8, 7, 4, 1, 2 }, 5, opponentPiece, slots));

                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[] { 0, 3, 4, 5, 2 }, 1, opponentPiece, slots));
                nearOpponentsBySlot.Add(GetOppentnNearCount(new int[] { 6, 3, 4, 5, 8 }, 7, opponentPiece, slots));
            }

            var maxCount = 0;
            var maxCountIndex = 0;
            for (int i = 0; i < nearOpponentsBySlot.Count; i++)
            {
                if (nearOpponentsBySlot[i] > maxCount)
                {
                    maxCount = nearOpponentsBySlot[i];
                    maxCountIndex = i;
                }
            }

            return new SlotMark()
            {
                Mark = (int) playPiece,
                Slot = _slotsToCheck[maxCountIndex]
            };
        }

        bool IfTwoInRow(int slot1, int slot2, int slot3, PieceType oppositePiece, PieceType[] slots)
        {
            if (slots[slot1] != PieceType.Empty && slots[slot2] != PieceType.Empty && slots[slot3] != PieceType.Empty)
            {
                return false;
            }

            if (slots[slot1] == oppositePiece && slots[slot2] == oppositePiece ||
                slots[slot1] == oppositePiece && slots[slot3] == oppositePiece ||
                slots[slot2] == oppositePiece && slots[slot3] == oppositePiece)
            {
                return true;
            }

            return false;
        }

        int GetEmpty(int slot1, int slot2, int slot3, PieceType[] slots)
        {
            if (slots[slot1] == PieceType.Empty)
            {
                return slot1;
            }
            if (slots[slot2] == PieceType.Empty)
            {
                return slot2;
            }
            if (slots[slot3] == PieceType.Empty)
            {
                return slot3;
            }

            return -1;
        }

        int GetOppentnNearCount(int[] slotsIndexes, int potentialMoveSlot, PieceType oppositePiece, PieceType[] slots)
        {
            int count = 0;

            if (slots[potentialMoveSlot] != PieceType.Empty)
                return -1;

            for (int i = 0; i < slotsIndexes.Length; i++)
            {
                if (slots[slotsIndexes[i]] == oppositePiece)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
