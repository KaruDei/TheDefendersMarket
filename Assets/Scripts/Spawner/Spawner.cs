using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnUnitsList = new();
    [SerializeField] private float _spawnDelay;

    private float _spawnTime;
    private Queue<GameObject> _spawnUnitsQueue = new();

    private void Start()
    {
        SetSpawnUnitsQueue();
    }

    private void Update()
    {
        if (_spawnUnitsQueue.Count <= 0) return;
         
        _spawnTime += Time.deltaTime;
        if (_spawnTime >= _spawnDelay)
        {
            _spawnTime = 0;
            UnitSpawn();
        }
    }

    private void SetSpawnUnitsQueue()
    {
        if (_spawnUnitsQueue.Count > 0) _spawnUnitsQueue.Clear();

            foreach (GameObject unit in _spawnUnitsList)
        {
            _spawnUnitsQueue.Enqueue(unit);
        }
    }

    private void UnitSpawn()
    {
        Instantiate(_spawnUnitsQueue.Dequeue());
    }
}
