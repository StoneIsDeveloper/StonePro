using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StonePro.Tests
{
    public sealed class Point : IComparable<Point>
    {
        private int m_x, m_y;
        public Point(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public int CompareTo(Point other)
        {
            return Math.Sign(Math.Sqrt(m_x*m_x + m_y*m_y*m_y) - Math.Sqrt(other.m_x*other.m_x + other.m_y*other.m_y));
        }
        public override string ToString()
        {
            return $"({m_x},{m_y})";
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Point[] points = new Point[] {
                new Point(3,3),
                new Point(1,2),
            };

            if (points[0].CompareTo(points[1]) > 0)
            {
                Point temp = points[0];
                points[0] = points[1];
                points[1] = temp;
            }

            Console.WriteLine("Points from closest to (0,0) to farthest:");
            foreach (Point p in points)
            {
                Console.WriteLine(p);
            }
            Console.ReadKey();
        }
    }
}
