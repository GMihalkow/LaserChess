using LaserChess.Core.Enums;
using UnityEngine.SceneManagement;

namespace LaserChess.Level
{
    public static class DifficultyManager
    {
        private static DifficultiesEnum difficulty;

        public static DifficultiesEnum Difficulty 
        {
            get => difficulty;
            set
            {
                if (SceneManager.GetActiveScene().name != "MenuScene") return;

                difficulty = value;
            }
        }
    }
}