using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Allows the Direction to change in a pirticular direction. </summary>
    public class Rotator: IRotator
    {
        /// <summary> Turns the direction to the right at 90 degrees. </summary>
        /// <param name="currentDirection">Direction to change.</param>
        /// <returns>New direction with 90 degrees right.</returns>
        public Direction TurnRight90Degress(Direction currentDirection)
        {
            int currentDirInt = (int)currentDirection;
            return (Direction)(++currentDirInt % 4);
        }
    }
}