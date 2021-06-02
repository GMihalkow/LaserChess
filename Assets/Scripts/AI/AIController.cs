using UnityEngine;

namespace LaserChess.AI
{
    public class AIController : MonoBehaviour
    {
        private GameObject _pieces;

        private void Awake()
        {
            this._pieces = GameObject.Find("AIPieces");
        }
    }
}