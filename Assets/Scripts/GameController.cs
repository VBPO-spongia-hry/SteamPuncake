using System;
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
            _tickables.Add(t);
            t.OnSpawn();
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
                try
                {
                    tickable.OnGameTick();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                
            }
        }
    }

    // call this function whenever you want to change bpm
    public void UpdateCombo(float combo)
    {
        // change bpm & update combo visualiser
        comboVisualiser.fillAmount = 1; // here put actual value
        if (combo==0){
            bpm = 100;
            //spusti soundtrack 100
            //zmen texturu srdca
        }
        if (combo==1){
            bpm = 120;
            //spusti soundtrack 120
            //zmen texturu srdca
        }
        if (combo==2){
            bpm = 140;
            //spusti soundtrack 140
            //zmen texturu srdca
        }
        if (combo==3){
            bpm = 160;
            //spusti soundtrack 160
            //zmen texturu srdca
        }
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
        var tickables = go.GetComponents<ITickable>();
        _tickables.AddRange(tickables);
        foreach (var t in tickables)
        {
            t.OnSpawn();
        }
        return go;
    }
}