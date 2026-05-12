using System;
using Microsoft.Xna.Framework;

namespace GameFramework;

public readonly struct Circle : IEquatable<Circle>
{
    private static readonly Circle s_empty = new Circle();

    /// <summary>
    /// 这个圆的圆心 x坐标
    /// </summary>
    public readonly int X;

    /// <summary>
    /// 这个圆的圆心 y坐标
    /// </summary>
    public readonly int Y;

    /// <summary>
    /// 此圆圈中心至边缘的长度（以像素为单位）。.
    /// </summary>
    public readonly int Radius;

    /// <summary>
    /// 获取此圆的圆心位置.
    /// </summary>
    public readonly Point Location => new Point(X, Y);

    /// <summary>
    /// 获取一个圆，其坐标为 X=0、Y=0，半径为 0 .
    /// </summary>
    public static Circle Empty => s_empty;

    /// <summary>
    /// 获取一个值，该值表示此圆是否具有半径为 0 且位置为 (0， 0) 的特征.
    /// </summary>
    public readonly bool IsEmpty => X == 0 && Y == 0 && Radius == 0;

    /// <summary>
    /// 获取此圆上最高点的 y 坐标.
    /// </summary>
    public readonly int Top => Y - Radius;

    /// <summary>
    /// 获取此圆上最低点的 y 坐标.
    /// </summary>
    public readonly int Bottom => Y + Radius;

    /// <summary>
    /// 获取此圆上最左侧点的 x 坐标.
    /// </summary>
    public readonly int Left => X - Radius;

    /// <summary>
    /// 获取此圆上最右侧点的 x 坐标.
    /// </summary>
    public readonly int Right => X + Radius;

    /// <summary>
    /// 根据指定的位置和半径创建一个新的圆.
    /// </summary>
    /// <param name="x">圆心的 x 坐标.</param>
    /// <param name="y">圆心的 y 坐标.</param>
    /// <param name="radius">从圆心到圆周边缘的长度.</param>
    public Circle(int x, int y, int radius)
    {
        X = x;
        Y = y;
        Radius = radius;
    }

    /// <summary>
    /// 根据指定的位置和半径创建一个新的圆.
    /// </summary>
    /// <param name="location">圆心位置.</param>
    /// <param name="radius">从圆心到圆周边缘的长度.</param>
    public Circle(Point location, int radius)
    {
        X = location.X;
        Y = location.Y;
        Radius = radius;
    }

    /// <summary>
    /// 返回一个值，该值表示指定的圆是否与当前圆相交。.
    /// </summary>
    /// <param name="other">另一个需要检查的圆圈.</param>
    /// <returns>如果另一个圆与当前圆相交，返回 true，反之则返回 false</returns>
    public bool Intersects(Circle other)
    {
        var radiiSquared = (Radius + other.Radius) * (Radius + other.Radius);
        var distanceSquared = Vector2.DistanceSquared(Location.ToVector2(), other.Location.ToVector2());
        return distanceSquared < radiiSquared;
    }

    /// <summary>
    /// 返回一个值，该值表示此圆与指定对象是否相等
    /// </summary>
    /// <param name="obj">与本圆进行比较的另一个对象.</param>
    /// <returns>如果本圆与指定对象相等，则返回 true.</returns>
    public override readonly bool Equals(object obj) => obj is Circle other && Equals(other);

    /// <summary>
    /// 返回一个值，该值表示此圆与指定的圆是否相等.
    /// </summary>
    /// <param name="other">与当前圆进行比较的另一个圆.</param>
    /// <returns>如果当前圆与指定圆相等，则返回 true.</returns>
    public readonly bool Equals(Circle other) => X == other.X && Y == other.Y && Radius == other.Radius;

    /// <summary>
    /// 返回此圆的哈希码.
    /// </summary>
    /// <returns>该圆的哈希码，以 32 位有符号整数形式呈现.</returns>
    public override readonly int GetHashCode() => HashCode.Combine(X, Y, Radius);

    /// <summary>
    /// Returns a value that indicates if the circle on the left hand side of the equality operator is equal to the
    /// circle on the right hand side of the equality operator.
    /// </summary>
    /// <param name="lhs">The circle on the left hand side of the equality operator.</param>
    /// <param name="rhs">The circle on the right hand side of the equality operator.</param>
    /// <returns>true if the two circles are equal; otherwise, false.</returns>
    public static bool operator ==(Circle lhs, Circle rhs) => lhs.Equals(rhs);

    /// <summary>
    /// Returns a value that indicates if the circle on the left hand side of the inequality operator is not equal to the
    /// circle on the right hand side of the inequality operator.
    /// </summary>
    /// <param name="lhs">The circle on the left hand side of the inequality operator.</param>
    /// <param name="rhs">The circle on the right hand side fo the inequality operator.</param>
    /// <returns>true if the two circle are not equal; otherwise, false.</returns>
    public static bool operator !=(Circle lhs, Circle rhs) => !lhs.Equals(rhs);
}