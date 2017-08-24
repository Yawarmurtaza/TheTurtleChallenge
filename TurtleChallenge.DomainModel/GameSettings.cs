using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TurtleChallenge.DomainModel
{
    /// <summary> Represents the initial settings of the game. </summary>
    [Serializable, XmlRoot(ElementName = "gameSettings")]
    public class GameSettings
    {
        /// <summary> Gets or sets the board of the game. </summary>
        [JsonProperty("boardSize")]
        [XmlElement(ElementName = "boardSize")]
        public GameBoard GameBoard { get; set; }

        /// <summary>
        /// Gets or sets the starting point of turtle.
        /// </summary>
        [JsonProperty("startingPosition")]
        [XmlElement(ElementName = "startingPosition")]
        public Point StartingPoint { get; set; }
        
        /// <summary> Gets or sets the direction of the turtle. </summary>
        [JsonProperty("direction")]
        [XmlElement(ElementName = "direction")]
        public Direction Direction { get; set; }

        /// <summary>
        /// Gets or sets the exit point of the turtle.
        /// </summary>
        [JsonProperty("exitPoint")]
        [XmlElement(ElementName = "exitPoint")]
        public Point ExitPoint { get; set; }

        /// <summary> Gets or sets the list of mines on the baord. </summary>
        [JsonProperty("mines")]
        [XmlElement("mine")]
        public List<Point> Mines { get; set; }

    }
}