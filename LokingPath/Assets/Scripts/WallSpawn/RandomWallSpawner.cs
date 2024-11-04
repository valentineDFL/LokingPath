using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Grid;
using Assets.Scripts.WallSpawn;
using UnityEngine;

public class RandomWallSpawner : MonoBehaviour, ISpawner, IWallChecker, IWallSpawner
{
    [SerializeField] private Transform _wallPrefab;
    [SerializeField] private int _wallCount;
    [SerializeField] private Transform _wallParentFolder;

    private System.Random _rnd = new System.Random();

    private List<Vector2Int> _wallsForSpawnPositions = new List<Vector2Int>();
    private List<Transform> _walls = new List<Transform>();

    public bool NodeIsWall(Vector2Int nodePositionOnGrid) => _wallsForSpawnPositions.Contains(nodePositionOnGrid);

    public void InitWallPositions(Vector2Int start, Vector2Int finish, Vector2Int gridSize)
    {
        int count = 0;
        while (count < _wallCount)
        {
            int indexX = _rnd.Next(0, gridSize.x - 1);
            int indexZ = _rnd.Next(0, gridSize.y - 1);

            Vector2Int result = new Vector2Int(indexX, indexZ);

            if (result != start && result != finish && !_wallsForSpawnPositions.Contains(result))
            {
                count++;
                _wallsForSpawnPositions.Add(result);
            }
        }
    }

    public void SetWallsPositions(IGridProvider gridCreator)
    {
        for (int i = 0; i < _wallsForSpawnPositions.Count; i++)
        {
            Vector2Int spawnPositionInGrid = _wallsForSpawnPositions[i];
            Vector3 spawnPosition = gridCreator.GetGridNode(spawnPositionInGrid.x, spawnPositionInGrid.y).WorldPosition;

            _walls[i].gameObject.SetActive(true);
            _walls[i].transform.position = spawnPosition;
        }

        _wallsForSpawnPositions.Clear();
    }

    public void Spawn()
    {
        for (int i = 0; i < _wallCount; i++)
        {
            GameObject wall = GameObject.Instantiate(_wallPrefab.gameObject, new Vector3(0, -15, 0), Quaternion.identity, _wallParentFolder);
            wall.SetActive(false);
            _walls.Add(wall.transform);
        }
    }
}
