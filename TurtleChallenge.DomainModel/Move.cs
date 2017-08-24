using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TurtleChallenge.DomainModel
{
    /// <summary>
    /// Defines the activity of the turtle.
    /// </summary>
    [XmlRoot(ElementName = "action")]
    public enum Move
    {
        /// <summary>
        /// Represents the move of the turtle. StepForward is more meaningful name, hence the mapping in the attributes.
        /// </summary>
        [EnumMember(Value = "move")]
        [XmlEnum(Name = "move")]
        StepForward = 0,
        
        /// <summary>
        /// Represents the rotation of turtle. Turn is more meaningful name, hence the mapping in the attributes.
        /// </summary>
        [EnumMember(Value = "rotate")]
        [XmlEnum(Name = "rotate")]
        Turn = 1
    }
}