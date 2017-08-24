using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Steps forward the turtle towards the East. </summary>
    public class EastMoveProcessor : IMoveProcessor
    {
        /// <summary> Gets the direction of the move. </summary>
        public Direction MoveDirection => Direction.East;

        /// <summary> Steps forward the turtle. </summary>
        /// <param name="board">Board on which turtle needs to move.</param>
        /// <param name="turtle">The turtle to move.</param>
        /// <returns>A value indicating if the turtle has successfully stepped forward. True the move has completed, false otherwise.</returns>
        public bool ProcessMove(GameBoard board, Turtle turtle)
        {
            if (turtle.Location.X < board.Columns - 1)
            {
                turtle.Location.X++;
                return true;
            }
            return false;
        }
    }
}