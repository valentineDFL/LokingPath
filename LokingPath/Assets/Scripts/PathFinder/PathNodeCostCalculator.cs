using UnityEngine;
using Assets.Scripts.GridNode;

namespace Assets.Scripts.PathFinder
{
    internal struct PathNodeCostCalculator
    {
        private Vector2Int _startPosition;
        private Vector2Int _endPosition;

        public PathNodeCostCalculator(Vector2Int startPosition, Vector2Int endPosition) // vector для удобства работы с двумерным массивом
        {
            _startPosition = startPosition;
            _endPosition = endPosition;
        }

        public void CalculateNodeCost(Node neighborNode, Node parentNode)
        {
            Vector2Int nodeXZ = neighborNode.PositionInGrid;

            float distance = Vector2Int.Distance(nodeXZ, parentNode.PositionInGrid) * 10;
            int g = parentNode.G + (int)distance;

            int h = CalculateManhettenDistance(nodeXZ, _endPosition);

            neighborNode.SetparentNode(parentNode);
            neighborNode.SetCost(g, h);
        }

        public int CalculateManhettenDistance(Vector2Int nodeXZ, Vector2Int position)
        {
            int x = position.x - nodeXZ.x;
            int z = position.y - nodeXZ.y;

            int manhettenDistance = Mathf.Abs(x) + Mathf.Abs(z);

            return manhettenDistance * 10;
        }
    }
}
