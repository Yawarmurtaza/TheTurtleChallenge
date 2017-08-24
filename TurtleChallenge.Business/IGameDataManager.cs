using System.Collections.Generic;
using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Provides the data for the game to setup and play. </summary>
    public interface IGameDataManager
    {
        /// <summary> Retrieves the startup game settings. </summary>
        /// <param name="filePath">Game settings file path.</param>
        /// <returns>Game settings.</returns>
        GameSettings GetGameSettings(string filePath);

        /// <summary> Retrieves the sequence of moves. </summary>
        /// <param name="filePath">Path to the file containing the sequences of moves.</param>
        /// <returns>Sequences containing the moves of the game.</returns>
        IEnumerable<Sequence> GetMoves(string filePath);
    }
}
