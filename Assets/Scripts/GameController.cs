using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int TPS;

    private List<ITickable> _tickables;
    private float _timer = 0;
    private float _nextTick = 0;
    
    
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
        _tickables = new List<ITickable>();
        foreach (var t in FindObjectsOfType<MonoBehaviour>().OfType<ITickable>())
        {
            var tickable = t;
            _tickables.Add(tickable);
            tickable.OnSpawn();
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _nextTick)
        {
            _nextTick += 1 / (float)TPS;
            
            foreach (var tickable in _tickables)
            {
                tickable.OnGameTick();
            }
        }
    }

    public void DestroyTickable(GameObject tickable)
    {
        if(tickable.GetComponent<ITickable>() != null)
            _tickables.Remove(tickable.GetComponent<ITickable>());
        Destroy(tickable);
    }
    
    public GameObject SpawnTickable(GameObject obj, Vector3 position, Quaternion rotation)
    {
        var go = Instantiate(obj, position, rotation);
        var tickable = go.GetComponent<ITickable>();
        _tickables.Add(tickable);
        tickable.OnSpawn();
        return go;
    }
}