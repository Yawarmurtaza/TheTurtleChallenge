using System.Collections.Generic;
using System.Xml.Serialization;

namespace TurtleChallenge.DomainModel
{
    /// <summary> Represents the root elemnt of xml document for game settings. </summary>
    [XmlRoot("sequences")]    
    public class GamePlay
    {
        /// <summary> Gets or sets the sequences of moves. </summary>
        [XmlElement("sequence")]
        public List<Sequence> SequenceList { get; set; }
    }
}