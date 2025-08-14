using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem.Pathfinding
{
    public class HexNode
    {
        public HexNode(HexCoordinates coordinates, Vector3 worldPosition, HexNode connectedNode, float terrainDifficulty, float g, float h)
        {
            Coordinates = coordinates;
            WorldPosition = worldPosition;
            ConnectedNode = connectedNode;
            TerrainDifficulty = terrainDifficulty;
            G = g;
            H = h;
        }

        public HexCoordinates Coordinates { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public HexNode ConnectedNode { get; private set; }

        //Pathfinding
        public float TerrainDifficulty { get; private set; }
        public float G { get; private set; } // Distance from the node to the start node
        public float H { get; private set; } // Distance from the node to the target node
        public float F => G + H;

        public void Connect(HexNode connectedNode)
        {
            ConnectedNode = connectedNode;
        }

        public void SetG(float g)
        {
            G = g;
        }

        public void SetH(float h)
        {
            H = h;
        }
    }
}