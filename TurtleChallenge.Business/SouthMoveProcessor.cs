using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Steps forward the turtle towards the South. </summary>
    public class SouthMoveProcessor : IMoveProcessor
    {
        /// <summary> Gets the direction of the move. </summary>
        public Direction MoveDirection => Direction.South;

        /// <summary> Steps forward the turtle. </summary>
        /// <param name="board">Board on which turtle needs to move.</param>
        /// <param name="turtle">The turtle to move.</param>
        /// <returns>A value indicating if the turtle has successfully stepped forward. True the move has completed, false otherwise.</returns>
        public bool ProcessMove(GameBoard board, Turtle turtle)
        {
            if (turtle.Location.Y < board.Rows -1)
            {
                turtle.Location.Y++;
                return true;
            }
            return false;
        }
    }
}