using System.Collections.Generic;

using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Allows the games rules to be validated. </summary>
    public interface IGameDataValidator
    {
        /// <summary> Validates that the initial game settings is valid. </summary>
        /// <param name="gameSettings">Game settings to validate.</param>
        /// <returns>Null if no errors found, list containing errors otherwise.</returns>
        IEnumerable<string> ValidateGameSettings(GameSettings gameSettings);
    }
}