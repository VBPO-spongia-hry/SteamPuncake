using System;
using System.Collections.Generic;
using System.Linq;
using Music;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public int bpm;
    public Animation heart;
    public Image comboVisualiser;
    public Image comboBackground;
    public Transform player;
    public Color[] comboColors;
    
    private List<ITickable> _tickables;
    private float _timer;
    private float _nextTick;
    private bool _paused;
    
    
    private void Start()
    {
        comboVisualiser.fillAmount = 0;
        combo=0;
        comboprogress=0;
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
    public float Beatdistance=>(_nextTick-_timer)/((float)bpm / 60);

    private void Update()
    {
        if (_paused) return;
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
    public int combo;
    public float comboprogress;
    public float baseBpm;

    public void Updatecomboprogress(int comboprogressChange){
        comboprogress += comboprogressChange;
        //Debug.Log (comboprogress);
        comboVisualiser.fillAmount = comboprogress/5; 
        // hodnota je z intervalu (0,1)
    }

    public void ResetCombo()
    {
        combo = 0;
        comboprogress = 0;
        UpdateCombo(0);
        Updatecomboprogress(0);
    }
    
    public void UpdateCombo(int comboChange)
    {
        // menenie Textury srdca
        
        // ak by si chcel menit aj farbu srdca (zafarbenej casti)
        
        // menenie rychlosti soundtracku
        // AudioEngine.SetTempo(multiplier);
        combo += comboChange;
        comboVisualiser.color = comboColors[combo];
        if (combo > 1)
            comboBackground.color = comboColors[combo - 1];
        if (combo==0){
            bpm = 70;
            
        }
        if (combo==1){
            bpm = 80;
            //spusti soundtrack 120
        }
        if (combo==2){
            bpm = 90;
            //spusti soundtrack 140
        }
        if (combo==3){
            bpm = 100;
            //spusti soundtrack 160
        }
        // AudioEngine.SetTempo(bpm / baseBpm);
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
    
    public void PauseGame()
    {
        PlayerMovement.DisableInput = true;
        GetComponent<AudioSource>().Pause();
        _paused = true;
    }

    public void UnpauseGame()
    {
        PlayerMovement.DisableInput = false;
        GetComponent<AudioSource>().UnPause();
        _paused = false;
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