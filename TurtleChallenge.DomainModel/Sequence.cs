using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TurtleChallenge.DomainModel
{
    /// <summary> Defines the sequence of moves. </summary>
    public class Sequence
    {
        /// <summary> Gets or sets the list of moves that turtle will take in the game board. </summary>
        [JsonProperty("moves", ItemConverterType = typeof(StringEnumConverter))]
        [XmlElement("action")]
        public List<Move> Moves { get; set; }

        /// <summary>
        /// Gets or sets the name of this sequence of moves.
        /// </summary>
        [JsonProperty("name")]
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
    }
}