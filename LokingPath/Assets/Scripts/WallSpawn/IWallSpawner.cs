using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Grid;
using UnityEngine;

namespace Assets.Scripts.WallSpawn
{
    public interface IWallSpawner
    {
        public void InitWallPositions(Vector2Int start, Vector2Int finish, Vector2Int gridSize);
        public void SetWallsPositions(IGridProvider gridCreator);
    }
}
