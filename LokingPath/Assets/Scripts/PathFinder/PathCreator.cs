using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.EntryPoint;
using Assets.Scripts.Grid;
using Assets.Scripts.GridNode;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.PathFinder
{
    public class PathCreator : MonoBehaviour
    {
        public event Action PathNotFounded;
        public event Action<IReadOnlyList<Vector3>> PathFounded;

        [SerializeField] private GameEntryPoint _entryPoint;
        
        private AStarPathFinder _pathFinder;
        private IReadOnlyList<Vector3> _route;

        private void OnEnable() => _entryPoint.OnGameInitialized += InitPath;
        private void OnDisable() => _entryPoint.OnGameInitialized -= InitPath;

        private void InitPath(IGridEndpoints position, IGridProvider creator)
        {
            Node start = position.StartPositionNode;
            Node finish = position.FinishPositionNode;

            _pathFinder = new AStarPathFinder(creator, start, finish);
            _route = _pathFinder.PathForMove();

            if (_route.Count != 0)
                PathFounded?.Invoke(_route);
            else
                PathNotFounded?.Invoke();
        }
    }
}