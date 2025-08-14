using System;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem
{
    [Serializable]
    public partial struct HexCoordinates
    {
        public HexCoordinates(int x, int z)
        {
            this.x = x;
            y = -x - z; // This because adding all three coordinates will always get to zero
            this.z = z;
        }

        [SerializeField] private int x;
        [SerializeField] private int y;
        [SerializeField] private int z;

        public int X => x;
        public int Y => y;
        public int Z => z;

        public string ToStringOnSeparateLines()
        {
            return $"{x}\n{y}\n{z}";
        }

        #region Static Methods

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }
        
        public static Vector2Int ToOffset(int x, int z)
        {
            return new Vector2Int(x * 2 + z, z);
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            var outerRadius = HexGridSettings.OuterRadius;
            var innerRadius = HexUtility.GetInnerRadius();
            var x = position.x / (innerRadius * 2f);//since while calculating horizontal coordinates we use diameter of the inner reach rather than radius we multiply by 2 to calculate diameter
            var y = -x; // While z coordinate is 0 x and y is mirror opposite of each other
            var offset = position.z /
                         (outerRadius *
                          3f); //Different between vertical rows is 1.5 times of outer radius. Since while calculating vertical coordinates we use diameter of the outer reach rather than radius we multiply by 2. 
            x -= offset;
            y -= offset;
            var iX = Mathf.RoundToInt(x);
            var iY = Mathf.RoundToInt(y);
            var iZ = Mathf.RoundToInt(-x - y);
            //The further away from a cell's center you get, the more rounding occurs.
            //This leads to trouble which coordinate got rounded in the wrong direction.
            //The solution then becomes to discard the coordinate with the largest rounding delta, and reconstruct it from the other two.
            //But as we only need X and Z, we don't need to bother with reconstructing Y.
            if (iX + iY + iZ != 0)
            {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x - y - iZ);

                if (dX > dY && dX > dZ)
                {
                    iX = -iY - iZ;
                }
                else if (dZ > dY)
                {
                    iZ = -iX - iY;
                }
            }

            return new HexCoordinates(iX, iZ);
        }

        #endregion

    }
}