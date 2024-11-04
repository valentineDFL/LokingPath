using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GridNode;
using Assets.Scripts.WallSpawn;

namespace Assets.Scripts.PathFinder
{
    internal class Grid
    {
        private Vector2Int _gridSize;

        private Vector3 _gridZeroPosition;

        private IWallChecker _wallChecher;

        private Node[,] _grid;

        public Grid(Vector2Int gridSize, Vector3 gridZeroPosition, IWallChecker wallChecher)
        {
            _gridSize = gridSize;
            _gridZeroPosition = gridZeroPosition;

            _wallChecher = wallChecher;

            _grid = InitializeGrid();
        }

        public Node GetGridNode(int inxexX, int indexZ) => _grid[inxexX, indexZ];
        public int GetGridLength(int demision) => _grid.GetLength(demision);

        private Node[,] InitializeGrid()
        {
            Node[,] grid = new Node[_gridSize.x, _gridSize.y];

            Vector3 xzPos = _gridZeroPosition;

            for (int x = 0; x < _gridSize.x; x++)
            {
                for(int z = 0; z < _gridSize.y; z++)
                {
                    InitGridNode(grid, new Vector2Int(x, z), xzPos);

                    xzPos += Vector3.forward;
                }

                xzPos = new Vector3(xzPos.x, _gridZeroPosition.y, _gridZeroPosition.z);
                xzPos += Vector3.right;
            }

            return grid;
        }

        private bool NodeIsRock(Vector2Int nodePositionInGrid)
        {
            return _wallChecher.NodeIsWall(nodePositionInGrid);

             // убрать рейкасты и заменить их на рандом рок спавн индексы
        }

        private void InitGridNode(Node[,] grid ,Vector2Int nodePositionInGrid, Vector3 xzPos)
        {
            bool nodeIsRock = NodeIsRock(nodePositionInGrid);

            Vector2Int gridXZ = new Vector2Int(nodePositionInGrid.x, nodePositionInGrid.y);

            grid[gridXZ.x, gridXZ.y] = new Node(gridXZ, xzPos, nodeIsRock);
        }
    }
}
