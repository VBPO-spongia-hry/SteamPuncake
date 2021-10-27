using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int bpm;
    public Animation heart;
    public Image comboVisualiser;
    
    private List<ITickable> _tickables;
    private float _timer;
    private float _nextTick;
    
    
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
            _nextTick += 1 / ((float)bpm / 60);
            heart.Play("Heartbeat");
            foreach (var tickable in _tickables)
            {
                tickable.OnGameTick();
            }
        }
    }

    // call this function whenever you want to change bpm
    public void UpdateCombo(float combo)
    {
        // change bpm & update combo visualiser
        comboVisualiser.fillAmount = 1; // here put actual value
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