using System;
using UnityEngine;

[Serializable]
public class Wave
{
    public int tickTriggered;
    public GameObject enemy;
}
public class EnemySpawner : MonoBehaviour, ITickable
{
    public Wave[] waves;

    private int _tickCount;
    private int _currentWave = 0;
    private Transform _player;
    public void OnGameTick()
    {
        _tickCount++;
        if(_currentWave >= waves.Length)
            GameController.Instance.DestroyTickable(gameObject);
        if (_tickCount == waves[_currentWave].tickTriggered)
        {
            var go = GameController.Instance.SpawnTickable(waves[_currentWave].enemy, transform.position,
                transform.rotation);
            _currentWave++;
            go.GetComponent<EnemyMovement>().player = _player;
           
        }
    }

    public void OnSpawn()
    {
        _player = GameController.Instance.player;
        _tickCount = 0;
    }
}