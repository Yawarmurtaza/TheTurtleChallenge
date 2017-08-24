using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Allows the Direction to change in a pirticular direction. </summary>
    public interface IRotator
    {
        Direction TurnRight90Degress(Direction currentDirection);
    }
}