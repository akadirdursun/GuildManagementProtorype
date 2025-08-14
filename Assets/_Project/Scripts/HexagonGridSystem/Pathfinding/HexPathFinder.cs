using System.Collections.Generic;
using System.Linq;
using ADK.Common;
using UnityEngine;

namespace AdventurerVillage.HexagonGridSystem.Pathfinding
{
    public class HexPathFinder : Singleton<HexPathFinder>
    {
        [SerializeField] private HexGridSettings hexGridSettings;
        [SerializeField] private HexMapData hexMapData;

        public List<HexNode> FindPath(HexCoordinates startCoordinates, HexCoordinates targetCoordinates)
        {
            List<HexNode> nodesToSearch = new();
            List<HexNode> nodes = new();
            HexNode selectedNode;
            CreateNode(startCoordinates, null, 0f);
            do
            {
                nodesToSearch = nodesToSearch.OrderBy(n => n.F).ThenBy(n => n.H).ToList();
                selectedNode = nodesToSearch[0];
                nodesToSearch.RemoveAt(0);

                if (selectedNode.Coordinates == targetCoordinates)
                    break;

                var neighbors = selectedNode.Coordinates.GetNeighbors();
                if (neighbors.Any(n => n.Coordinates == targetCoordinates))
                {
                    var node = CreateNode(targetCoordinates, selectedNode, 0f);
                    selectedNode = node;
                    break;
                }
                
            } while (true);

            var path = new List<HexNode>();
            while (selectedNode != null)
            {
                path.Add(selectedNode);
                selectedNode = selectedNode.ConnectedNode;
            }

            path.Reverse();
            return path;

            HexNode CreateNode(HexCoordinates coordinates, HexNode connectedNode, float terrainDifficulty)
            {
                var node = nodes.FirstOrDefault(n => n.Coordinates == coordinates);
                if (node == null)
                {
                    var worldPos = coordinates.ToWorldPosition();
                    //Later when you add terrain effects, add terrain difficulty to the previous node's G
                    var g = connectedNode != null ? connectedNode.G + terrainDifficulty : 0;
                    var h = coordinates.HexDistanceTo(targetCoordinates);
                    node = new HexNode(coordinates, worldPos, connectedNode, terrainDifficulty, g, h);
                    nodesToSearch.Add(node);
                    nodes.Add(node);
                    return node;
                }

                //Later when you add terrain effects, add terrain difficulty to the previous node's G
                var newG = connectedNode != null ? connectedNode.G + terrainDifficulty : 0;
                if (node.G - newG >= 1f)
                {
                    node.Connect(connectedNode);
                    node.SetG(newG);
                    nodesToSearch.Add(node);
                }

                return node;
            }
        }
    }
}