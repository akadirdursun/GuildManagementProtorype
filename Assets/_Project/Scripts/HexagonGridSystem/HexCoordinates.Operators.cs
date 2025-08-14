using System;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    public partial struct HexCoordinates : IEquatable<HexCoordinates>
    {
        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public bool Equals(HexCoordinates other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override bool Equals(object obj)
        {
            return obj is HexCoordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public static HexCoordinates operator +(HexCoordinates left, HexCoordinates right)
        {
            return new HexCoordinates(left.X + right.X, left.Z + right.Z);
        }

        public static HexCoordinates operator +(HexCoordinates left, Vector2Int right)
        {
            return new HexCoordinates(left.X + right.x, left.Z + right.y);
        }

        public static HexCoordinates operator +(Vector2Int left, HexCoordinates right)
        {
            return right + left;
        }

        public static bool operator ==(HexCoordinates left, HexCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(HexCoordinates left, HexCoordinates right)
        {
            return !left.Equals(right);
        }
    }
}