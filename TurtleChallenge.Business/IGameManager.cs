using System.Collections.Generic;

namespace TurtleChallenge.Business
{
    /// <summary> Allows the game to start with game settings and moves from the files. </summary>
    public interface IGameManager
    {
        /// <summary> Starts the game. </summary>
        /// <param name="gameSettingsFilePath">File path to the game initial settings file.</param>
        /// <param name="moveSequenceFilePath">File path to the moves containing the sequences.</param>
        /// <returns>Collection of strings containing the outcome for each sequence in the moves file.</returns>
        IEnumerable<string> Start(string gameSettingsFilePath, string moveSequenceFilePath);
    }
}