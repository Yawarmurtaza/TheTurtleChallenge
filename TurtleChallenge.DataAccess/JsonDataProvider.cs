using System.Collections.Generic;
using Newtonsoft.Json;
using TurtleChallenge.DomainModel;

namespace TurtleChallenge.DataAccess
{
    /// <summary> Provides the data from json files. </summary>
    public class JsonDataProvider : IDataProvider
    {
        private readonly IFileReadonlyAccess fileAccess;

        /// <summary> Gets the extension of the file for which the data provider will be selected. </summary>
        public string Extension => ".json";

        public JsonDataProvider(IFileReadonlyAccess fileAccess)
        {
            this.fileAccess = fileAccess;
        }

        /// <summary> Gets the game settings from the file. </summary>
        /// <param name="filePath">Game settings file path.</param>
        /// <returns>Game settings from the file.</returns>
        public GameSettings GetGameSettings(string filePath)
        {
            string gameSettingsJson = this.fileAccess.ReadAllText(filePath);
            GameSettings gameSettings = JsonConvert.DeserializeObject<GameSettings>(gameSettingsJson);
            return gameSettings;

        }
        
        /// <summary> Gets all the movies defined in the moves file. </summary>
        /// <param name="filePath">Move file path.</param>
        /// <returns>Moves from the file.</returns>
        public IEnumerable<Sequence> GetAllMoves(string filePath)
        {
            string movesJson = this.fileAccess.ReadAllText(filePath);
            IEnumerable<Sequence> sequences = JsonConvert.DeserializeObject<IEnumerable<Sequence>>(movesJson);
            return sequences;
        }
    }
}
