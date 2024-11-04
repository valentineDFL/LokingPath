using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.PathFinder;
using Assets.Scripts.GridNode;
using Assets.Scripts.Grid;
using Assets.Scripts.WallSpawn;

namespace Assets.Scripts.PathFinder
{
    public class GridCreator : MonoBehaviour, IGridProvider, IGridEndpoints
    {
        [SerializeField] private IRouteEndpoints _gridEndpoints;
        private Assets.Scripts.PathFinder.Grid _grid;

        public Node StartPositionNode => _grid.GetGridNode(_gridEndpoints.StartNodePosition.x, _gridEndpoints.StartNodePosition.y);
        public Node FinishPositionNode => _grid.GetGridNode(_gridEndpoints.FinishNodePosition.x, _gridEndpoints.FinishNodePosition.y);

        public void InitGrid(IWallChecker wallChecher, IRouteEndpoints gridEndPoints)
        {
            _gridEndpoints = gridEndPoints;
            Grid grid = new Grid(gridEndPoints.GridSize, gridEndPoints.GridZeroPosition, wallChecher);
            _grid = grid;
        }

        public Node GetGridNode(int indexX, int inxedZ) => _grid.GetGridNode(indexX, inxedZ);
        public int GetGridLength(int demision) => _grid.GetGridLength(demision);

        private void OnDrawGizmos()
        {
            if (_grid == null) return;

            for (int i = 0; i < _grid.GetGridLength(0); i++)
            {
                for (int j = 0; j < _grid.GetGridLength(1); j++)
                {
                    if (_grid.GetGridNode(i, j).IsWall)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawWireCube(_grid.GetGridNode(i, j).WorldPosition + Vector3.up, new Vector3(0.9f, 0.9f, 0.9f));
                    }
                    
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireSphere(_grid.GetGridNode(_gridEndpoints.StartNodePosition.x, _gridEndpoints.StartNodePosition.y).WorldPosition + Vector3.up, 0.3f);

                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireSphere(_grid.GetGridNode(_gridEndpoints.FinishNodePosition.x, _gridEndpoints.FinishNodePosition.y).WorldPosition + Vector3.up, 0.3f);
                }
            }
        }
    }
}
