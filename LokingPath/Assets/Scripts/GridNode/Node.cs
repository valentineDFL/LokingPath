using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GridNode
{
    public class Node
    {
        public const int DistanceBetweenNeighborNode = 10;

        public Vector2Int PositionInGrid { get; private set; }
        public Vector3 WorldPosition { get; private set; }
        public bool IsWall { get; private set; }

        public int G { get; private set; }
        public int H { get; private set; }
        public int FCost
        {
            get { return G + H; }
            private set { FCost = value; }
        }

        public Node ParentNode { get; private set; }

        public Node(Vector2Int positionInGrid, Vector3 worldPosition, bool isRock)
        {
            PositionInGrid = positionInGrid;
            WorldPosition = worldPosition;
            IsWall = isRock;
        }

        public void SetparentNode(Node parentNode) => ParentNode = parentNode;

        public void SetCost(int g, int h)
        {
            G = g;
            H = h;
        }
    }
}
