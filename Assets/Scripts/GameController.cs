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
    public Transform player;
    
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
            ITickable[] currentTickables =new ITickable[_tickables.Count];
            _tickables.CopyTo(currentTickables);
            foreach (var tickable in currentTickables)
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

    public void RegisterTickable(GameObject tickable)
    {
        _tickables.Add(tickable.GetComponent<ITickable>());
        tickable.GetComponent<ITickable>().OnSpawn();
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