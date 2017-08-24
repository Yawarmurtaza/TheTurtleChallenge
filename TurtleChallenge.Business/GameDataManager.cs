using System.Collections.Generic;
using System.IO;
using System.Linq;
using TurtleChallenge.DataAccess;
using TurtleChallenge.DomainModel;

namespace TurtleChallenge.Business
{
    /// <summary> Provides the data for the game to setup and play. </summary>
    public class GameDataManager : IGameDataManager
    {
        private readonly IDictionary<string, IDataProvider> dataProviderMap;

        public GameDataManager(IDataProvider[] dataProviders)
        {
            this.dataProviderMap = dataProviders.ToDictionary(p => p.Extension, p => p);
        }

        /// <summary> Retrieves the startup game settings. </summary>
        /// <param name="filePath">Game settings file path.</param>
        /// <returns>Game settings.</returns>
        public GameSettings GetGameSettings(string filePath)
        {
            IDataProvider provider = this.GetDataProvider(filePath);
            return provider.GetGameSettings(filePath);
        }

        /// <summary> Retrieves the sequence of moves. </summary>
        /// <param name="filePath">Path to the file containing the sequences of moves.</param>
        /// <returns>Sequences containing the moves of the game.</returns>
        public IEnumerable<Sequence> GetMoves(string filePath)
        {
            IDataProvider provider = this.GetDataProvider(filePath);
            return provider.GetAllMoves(filePath);
        }

        /// <summary> Returns a file extension matching data provider. </summary>
        /// <param name="filePath">File containing the game data.</param>
        /// <returns>Approperiate data provider.</returns>
        private IDataProvider GetDataProvider(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            IDataProvider provider = dataProviderMap[file.Extension];
            return provider;
        }
    }
}