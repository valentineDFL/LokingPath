using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GridNode
{
    public class NodeVector : MonoBehaviour, IRouteEndpoints
    {
        [SerializeField] private Vector2Int _startPosition;
        [SerializeField] private Vector2Int _finishPosition;

        [SerializeField] private Transform _gridSize;
        [SerializeField] private Transform _gridZeroPosition;

        private System.Random _rnd = new System.Random();

        public Vector2Int StartNodePosition => _startPosition;
        public Vector2Int FinishNodePosition => _finishPosition;

        public Vector2Int GridSize => new Vector2Int((int)_gridSize.localScale.x, (int)_gridSize.localScale.z);
        public Vector3 GridZeroPosition => _gridZeroPosition.position;

        public void Init()
        {
            Vector3 scale = _gridSize.localScale;

            int startX = CheckPositionsSize(_startPosition.x, (int)scale.x - 1);
            int startZ = CheckPositionsSize(_startPosition.y, (int)scale.z - 1);

            _startPosition = new Vector2Int(startX, startZ);
            _finishPosition = RandomizeFinishPosition();
        }

        private int CheckPositionsSize(int positionOnGrid, int scale)
        {
            if (positionOnGrid > scale) // 10 > 5
                return scale;
            else if (positionOnGrid < 0) // -1 < 0
                return 0;
            else                         // 5 из 10
                return positionOnGrid;
        }

        private Vector2Int RandomizeFinishPosition()
        {
            Vector3 scale = _gridSize.localScale;

            while (true)
            {
                int finishX = _rnd.Next(0, (int)scale.x);
                int finishZ = _rnd.Next(0, (int)scale.z);

                Vector2Int resultFinishPosition = new Vector2Int(finishX, finishZ);

                if(resultFinishPosition != _startPosition)
                    return resultFinishPosition;
            }
        }
    }
}
