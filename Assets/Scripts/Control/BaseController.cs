using LaserChess.Level;
using LaserChess.Pieces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaserChess.Control
{
    public abstract class BaseController : MonoBehaviour
    {
        [SerializeField] LevelPiecesSetup[] _piecesConfig;

        protected GameObject _pieces;

        private void Start()
        {
            foreach (var config in this._piecesConfig)
            {
                if (config.Difficulty != DifficultyManager.Difficulty) continue;

                foreach (var piece in config.Pieces)
                {
                    var pieceInstance = GameObject.Instantiate(piece.Piece, this._pieces.transform);
                    pieceInstance.GetComponent<BasePiece>().SetupPosition(piece.Row, piece.Col);
                }
            }
        }
    }
}