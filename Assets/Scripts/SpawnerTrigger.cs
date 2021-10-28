using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SpawnerTrigger : MonoBehaviour
    {
        public GameObject[] spawners;

        private void Start()
        {
            foreach (var spawner in spawners)
            {
                spawner.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (var spawner in spawners)
                {
                    spawner.SetActive(true);
                    GameController.Instance.RegisterTickable(spawner);
                }
            }
        }
    }
}