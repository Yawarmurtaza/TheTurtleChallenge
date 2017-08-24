using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Steps forward the turtle in a pirticular direction. </summary>
    public interface IMoveProcessor
    {
        /// <summary> Gets the direction of the move. </summary>
        Direction MoveDirection { get; }

        /// <summary> Steps forward the turtle. </summary>
        /// <param name="board">Board on which turtle needs to move.</param>
        /// <param name="turtle">The turtle to move.</param>
        /// <returns>A value indicating if the turtle has successfully stepped forward. True the move has completed, false otherwise.</returns>
        bool ProcessMove(GameBoard board, Turtle turtle);
    }
}