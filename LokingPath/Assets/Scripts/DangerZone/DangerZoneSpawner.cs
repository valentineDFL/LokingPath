using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.PathFinder;
using UnityEngine;

namespace Assets.Scripts.DangerZone
{
    public class DangerZoneSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private Transform _dangerZonePrefab;
        [SerializeField] private Transform _dangerZoneParentFolder;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] [Tooltip("Количество зон с запасом")] private int _dangerZonesCount;

        [SerializeField] private int _divider = 5;

        private System.Random _rnd = new System.Random();

        private List<Transform> _dangerZones = new List<Transform>();
        private List<Vector3> _dangerZonesPositions = new List<Vector3>();

        private Vector3 _defaultPosition = new Vector3(0, -15, 0);

        private void OnEnable() => _pathCreator.PathFounded += Init;

        private void OnDisable() => _pathCreator.PathFounded -= Init;

        private void Init(IReadOnlyList<Vector3> route)
        {
            DangerZoneCountRound(route.Count);

            InitDangerZonePositions(route);
            SetDangerZonesPositions();
        }

        private void DangerZoneCountRound(int routeCount)
        {
            int newCount = routeCount;
            while (newCount % _divider != 0)
            {
                newCount--;
            }
            _dangerZonesCount = newCount / _divider;
        }

        private void InitDangerZonePositions(IReadOnlyList<Vector3> route)
        {
            while (_dangerZonesPositions.Count != _dangerZonesCount)
            {
                int positionIndex = _rnd.Next(0, route.Count - 1);

                if (positionIndex == 0 || positionIndex == route.Count - 1)
                    continue;

                if (_dangerZonesPositions.Contains(route[positionIndex]))
                    continue;

                _dangerZonesPositions.Add(route[positionIndex]);
            }
        }

        private void SetDangerZonesPositions()
        {
            for(int i = 0; i < _dangerZones.Count; i++)
            {
                if(i < _dangerZonesPositions.Count)
                {
                    _dangerZones[i].gameObject.SetActive(true);
                    _dangerZones[i].transform.position = _dangerZonesPositions[i];
                }
                else
                {
                    _dangerZones[i].gameObject.SetActive(false);
                    _dangerZones[i].transform.position = _defaultPosition;
                }
            }

            _dangerZonesPositions.Clear();
        }

        public void Spawn()
        {
            for (int i = 0; i < _dangerZonesCount; i++)
            {
                GameObject dangerZone = GameObject.Instantiate(_dangerZonePrefab.gameObject, _defaultPosition, Quaternion.identity, _dangerZoneParentFolder);
                dangerZone.name = $"{dangerZone.name + " " + i}";
                dangerZone.SetActive(false);
                _dangerZones.Add(dangerZone.transform);
            }
        }
    }
}