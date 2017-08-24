using System.Collections.Generic;

namespace TurtleChallenge.DomainModel
{
    public class PointComparer : IEqualityComparer<Point>
    {
        public bool Equals(Point x, Point y)
        {
            return x.Y == y.Y && x.X == y.X;
        }

        public int GetHashCode(Point obj)
        {
            return obj.GetHashCode();
        }
    }
}