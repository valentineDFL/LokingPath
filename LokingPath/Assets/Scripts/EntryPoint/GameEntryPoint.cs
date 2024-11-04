using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DangerZone;
using Assets.Scripts.FinishTrigger;
using Assets.Scripts.Grid;
using Assets.Scripts.GridNode;
using Assets.Scripts.HeroHealth;
using Assets.Scripts.Move;
using Assets.Scripts.PathFinder;
using Assets.Scripts.ShieldAbility;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.EntryPoint
{
    internal class GameEntryPoint : MonoBehaviour
    {
        public event Action<IGridEndpoints, IGridProvider> OnGameInitialized;
        public event Action OnLevelStarted;

        [SerializeField] [Header("Entry")] private NodeVector _nodeVector;
        [SerializeField] private GridCreator _gridCreator;
        [SerializeField] private RandomWallSpawner _wallSpawner;
        [SerializeField] private DangerZoneSpawner _dangerZoneSpawner;

        [SerializeField] [Header("PathCreator")] private PathCreator _pathCreator;

        [SerializeField] [Header("FinishTrigger")] private Transform _finishTriggerParent;
        private FinishTrigger.FinishTrigger _finishTrigger;

        [SerializeField] [Header("HeroHealth")] private HeroHealth.HeroHealth _heroHealth;

        private void OnEnable()
        {
            _finishTrigger = _finishTriggerParent.GetComponentInChildren<FinishTrigger.FinishTrigger>();

            _pathCreator.PathNotFounded += InitGame;
            _finishTrigger.GameFinished += InitGame;
            _heroHealth.OnHeroDeaded += InitGame;
        }

        private void OnDisable()
        {
            _pathCreator.PathNotFounded -= InitGame;
            _finishTrigger.GameFinished -= InitGame;
            _heroHealth.OnHeroDeaded -= InitGame;
        }

        private void Awake()
        {
            _wallSpawner.Spawn();
            _dangerZoneSpawner.Spawn();
        }

        private void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            OnLevelStarted?.Invoke();

            _nodeVector.Init();

            _wallSpawner.InitWallPositions(_nodeVector.StartNodePosition, _nodeVector.FinishNodePosition, _nodeVector.GridSize);
            _gridCreator.InitGrid(_wallSpawner, _nodeVector);
            
            Vector3 finishTriggerSpawnPosition = _gridCreator.FinishPositionNode.WorldPosition;
            _finishTriggerParent.gameObject.transform.position = finishTriggerSpawnPosition;

            _wallSpawner.SetWallsPositions(_gridCreator);
            OnGameInitialized?.Invoke(_gridCreator, _gridCreator);
        }
    }
}
