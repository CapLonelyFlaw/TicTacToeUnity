using UnityEngine;
using System.Collections;

namespace GameCore
{
    public enum GameStateType
    {
        None,
        InProgress,
        OWins,
        XWins,
        Tie
    }

    public enum PieceType
    {
        Empty = 0,
        O,
        X
    }

    public enum PlayerStateType
    {
        None,
        Win,
        Loose,
        Tie
    }

    /// <summary>
    /// Difficulty type - Name = DepthOfAiSearch 
    /// </summary>
    public enum DifficultyType
    {
        Easy = 1,
        Normal = 5,
        Invincible = 10
    }
}
