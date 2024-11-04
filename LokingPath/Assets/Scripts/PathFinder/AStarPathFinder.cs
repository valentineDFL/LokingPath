using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using Assets.Scripts.GridNode;
using Assets.Scripts.Grid;

namespace Assets.Scripts.PathFinder
{
    internal class AStarPathFinder
    {
        public event Action PathNotFinded;

        private const int ArrowSides = 4;

        private Node _startNode;
        private Node _finishNode;

        private IGridProvider _gridCreator;
        private List<Node> _openList;
        private List<Node> _closeList;

        private PathNodeCostCalculator _betweenNodeCalculator;

        public AStarPathFinder(IGridProvider gridCreator, Node startNode, Node finishNode)
        {
            _gridCreator = gridCreator;
            _startNode = startNode;
            _finishNode = finishNode;
            _betweenNodeCalculator = new PathNodeCostCalculator(_startNode.PositionInGrid, _finishNode.PositionInGrid);

            _openList = new List<Node>() { _startNode };
            _closeList = new List<Node>();
        }

        public IReadOnlyList<Vector3> PathForMove()
        {
            GridResearch();

            List<Vector3> finalPath = new List<Vector3>();

            if (_finishNode.ParentNode != null)
            {
                Node node = _finishNode;
                finalPath.Add(node.WorldPosition);
                while (node.ParentNode != null)
                {
                    node = node.ParentNode;
                    finalPath.Add(node.WorldPosition);
                }
            }

            finalPath.Reverse();

            return finalPath;
        }

        private void GridResearch() // + have 4 side
        {
            int h = _betweenNodeCalculator.CalculateManhettenDistance(_openList[0].PositionInGrid, _finishNode.PositionInGrid);
            _openList[0].SetCost(0, h);

            while(_openList.Count > 0)
            {
                Node currentNode = GetLowestFcostPoint(_openList);

                _openList.Remove(currentNode);
                _closeList.Add(currentNode);

                if (currentNode == _finishNode) return;

                foreach(Node neighbourNode in GetNodeNeighbours(currentNode))
                {
                    if (_closeList.Contains(neighbourNode) || neighbourNode.IsWall)
                        continue;

                    if (_openList.Contains(neighbourNode))
                        continue;

                    if (!_openList.Contains(neighbourNode))
                    {
                        _betweenNodeCalculator.CalculateNodeCost(neighbourNode, currentNode);
                        _openList.Add(neighbourNode);
                    }
                }
            }
        }

        private IReadOnlyList<Node> GetNodeNeighbours(Node currentNode)
        {
            List<Node> neighbours = new List<Node>();

            for (int i = 0; i < ArrowSides; i++)
            {
                Vector2Int neighbourPosition = GetNeighbourPosition(i, currentNode.PositionInGrid);
                bool xOutOfRange = (neighbourPosition.x < 0 || neighbourPosition.x > _gridCreator.GetGridLength(0) - 1);
                bool yOutOfRange = (neighbourPosition.y < 0 || neighbourPosition.y > _gridCreator.GetGridLength(1) - 1);

                if (xOutOfRange || yOutOfRange)
                    continue;

                Node neighbour = _gridCreator.GetGridNode(neighbourPosition.x, neighbourPosition.y);
                neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private Vector2Int GetNeighbourPosition(int index, Vector2Int currentPosition)
        {
            switch (index)
            {
                case 0:
                    return currentPosition + Vector2Int.up;
                case 1:
                    return currentPosition + Vector2Int.right;
                case 2:
                    return currentPosition + Vector2Int.down;
                case 3:
                    return currentPosition + Vector2Int.left;
                default:
                    throw new ArgumentException("Index biggest or lowest of 0-3");
            }
        }

        private Node GetLowestFcostPoint(List<Node> openList) => openList.OrderBy(x => x.FCost).First();
    }
}