using System.Collections.Generic;
using TurtleChallenge.DomainModel;

namespace TurtleChallenge.DataAccess
{
    /// <summary>
    /// Allows the access to the data file.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary> Gets the extension of the file for which the data provider will be selected. </summary>
        string Extension { get; }

        /// <summary> Gets the game settings from the file. </summary>
        /// <param name="filePath">Game settings file path.</param>
        /// <returns>Game settings from the file.</returns>
        GameSettings GetGameSettings(string filePath);

        /// <summary> Gets all the movies defined in the moves file. </summary>
        /// <param name="filePath">Move file path.</param>
        /// <returns>Moves from the file.</returns>
        IEnumerable<Sequence> GetAllMoves(string filePath);
    }
}