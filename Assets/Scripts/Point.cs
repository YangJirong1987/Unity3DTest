using UnityEngine;
using System.Collections;
/// <summary>
/// 结构位置
/// </summary>
public class Point  {

    private static Point zeroPoint = default(Point);
    public int X;
    public int Y;
    public static Point Zero
    {
        get
        {
            return Point.zeroPoint;
        }
    }
    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    public static bool operator ==(Point a, Point b)
    {
        return a.Equals(b);
    }
    public static bool operator !=(Point a, Point b)
    {
        return !a.Equals(b);
    }
    public bool Equals(Point other)
    {
        return this.X == other.X && this.Y == other.Y;
    }
    public override bool Equals(object obj)
    {
        return obj is Point && this.Equals((Point)obj);
    }
    public override int GetHashCode()
    {
        return this.X ^ this.Y;
    }
    public override string ToString()
    {
        return string.Format("{{X:{0} Y:{1}}}", new object[]
			{
				this.X,
				this.Y
			});
    }
}
