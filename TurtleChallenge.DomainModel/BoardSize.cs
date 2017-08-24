using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TurtleChallenge.DomainModel
{
    /// <summary> Represents the board of square of rows and columns on which the turtle will navigate. </summary>
    [XmlRoot(ElementName = "boardSize")]
    public class GameBoard
    {
        /// <summary>
        /// Gets or sets the number of columns of this game board. This value cannot be a negative number.
        /// </summary>
        [JsonProperty("columns")]
        [XmlElement(ElementName = "columns")]
        public uint Columns { get; set; }

        /// <summary>
        /// Represents the number of rows of this game board. This value cannot be a negative number.
        /// </summary>
        [JsonProperty("rows")]
        [XmlElement(ElementName = "rows")]
        public uint Rows { get; set; }

    }
}