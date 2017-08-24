using System;
using System.Xml.Serialization;

namespace TurtleChallenge.DomainModel
{
    /// <summary> Represents the direction of the turtle. </summary>
    public enum Direction
    {
        /// <summary> The North direction. </summary>
        [XmlEnum(Name = "North")]
        North = 0,
       
        /// <summary> The East direction. </summary>
        [XmlEnum(Name  = "East")]
        East = 1,

        /// <summary> The South direction. </summary>
        [XmlEnum(Name = "South")]
        South = 2,

        /// <summary> The west direction. </summary>
        [XmlEnum(Name  = "West")]
        West = 3
    }
}