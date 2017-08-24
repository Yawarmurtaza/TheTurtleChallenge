using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Steps forward the turtle towards the West. </summary>
    public class WestMoveProcessor : IMoveProcessor
    {
        /// <summary> Gets the direction of the move. </summary>
        public Direction MoveDirection => Direction.West;

        /// <summary> Steps forward the turtle. </summary>
        /// <param name="board">Board on which turtle needs to move.</param>
        /// <param name="turtle">The turtle to move.</param>
        /// <returns>A value indicating if the turtle has successfully stepped forward. True the move has completed, false otherwise.</returns>
        public bool ProcessMove(GameBoard board, Turtle turtle)
        {
            if (turtle.Location.X > 0)
            {
                turtle.Location.X--;
                return true;
            }

            return false;
        }
    }
}