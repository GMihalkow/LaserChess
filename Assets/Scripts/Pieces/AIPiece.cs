using LaserChess.Pieces.Contracts;

namespace LaserChess.Pieces
{
    public abstract class AIPiece : BasePiece, IAIPiece
    {
        public abstract void Move();
    }
}