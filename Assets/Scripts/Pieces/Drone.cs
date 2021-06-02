namespace LaserChess.Pieces
{
    public class Drone : AIPiece
    {
        public override void Move()
        {
            this._mover.Move(this._mover.CurrentRow - 1, this._mover.CurrentCol);
        }
    }
}