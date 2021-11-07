using System;
using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    public GameObject[] spawners;
    public bool optional;
    public bool Activated { get; private set; }

    private void Start()
    {
        Activated = false;
        foreach (var spawner in spawners)
        {
            spawner.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ! Activated)
        {
            Activated = true;
            foreach (var spawner in spawners)
            {
                spawner.SetActive(true);
                GameController.Instance.RegisterTickable(spawner);
            }
        }
    }
}