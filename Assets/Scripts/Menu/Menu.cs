using LaserChess.Core.Enums;
using LaserChess.Level;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LaserChess.Menu
{
    /// <summary>
    /// Methods are called from editor
    /// </summary>
    public class Menu : MonoBehaviour
    {
        [SerializeField] Dropdown _difficultyDropdown;

        private const int DEFAULT_DROPDOWN_INDEX = 0;

        public void StartGame()
        {
            if (this._difficultyDropdown.value == DEFAULT_DROPDOWN_INDEX) return;

            DifficultyManager.Difficulty = (DifficultiesEnum)this._difficultyDropdown.value;
            SceneManager.LoadSceneAsync("PlayScene", LoadSceneMode.Single);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}