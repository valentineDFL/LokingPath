using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GridNode
{
    public interface IRouteEndpoints
    {
        public Vector2Int StartNodePosition { get; }
        public Vector2Int FinishNodePosition { get; }

        public Vector2Int GridSize { get; }
        public Vector3 GridZeroPosition { get; }
    }
}
