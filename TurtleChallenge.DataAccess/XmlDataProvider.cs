using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TurtleChallenge.DomainModel;

namespace TurtleChallenge.DataAccess
{
    /// <summary> Provides the data from xml files. </summary>
    public class XmlDataProvider : IDataProvider
    {
        private readonly IFileReadonlyAccess fileAccess;

        /// <summary> Gets the extension of the file for which the data provider will be selected. </summary>
        public string Extension => ".xml";
        public XmlDataProvider(IFileReadonlyAccess fileAccess)
        {
            this.fileAccess = fileAccess;
        }

        /// <summary> Gets the game settings from the file. </summary>
        /// <param name="filePath">Game settings file path.</param>
        /// <returns>Game settings from the file.</returns>
        public GameSettings GetGameSettings(string filePath)
        {
            string gameSettingsXml = this.fileAccess.ReadAllText(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(GameSettings));
            using (StringReader reader = new StringReader(gameSettingsXml))
            {
                GameSettings gameSettings = (GameSettings)serializer.Deserialize(reader);
                return gameSettings;
            }
        }
        
        /// <summary> Gets all the movies defined in the moves file. </summary>
        /// <param name="filePath">Move file path.</param>
        /// <returns>Moves from the file.</returns>
        public IEnumerable<Sequence> GetAllMoves(string filePath)
        {
            string movesXml = this.fileAccess.ReadAllText(filePath);
            
            XmlSerializer serializer = new XmlSerializer(typeof(GamePlay));
            using (StringReader reader = new StringReader(movesXml))
            {
                GamePlay gamePlay = (GamePlay)serializer.Deserialize(reader);

                return gamePlay.SequenceList;
            }
        }

    }
}