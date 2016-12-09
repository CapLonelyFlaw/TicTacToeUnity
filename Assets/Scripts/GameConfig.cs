using System;
using UnityEngine;

namespace GameCore
{
    public class GameConfig : ScriptableSingleton<GameConfig>
    {
        [Header("View Settings")]
        public string P1Name = "Human";
        public string P2Name = "Computer";

        [Header("Scene Settings")]
        public string MainSceneName = "MainMenuScene";
        public string GameSceneName = "GameScene";

        [Header("AI search depths")]
        public int EasyDepth = 1;
        public int NormalDepth = 5;
        public int HardDepth = 10;

        public int GetSearchDepthByDifficulty(DifficultyType difficulty)
        {
            if (difficulty == DifficultyType.Easy)
                return EasyDepth;
            if (difficulty == DifficultyType.Normal)
                return NormalDepth;

            return HardDepth;
        }
    }
}
