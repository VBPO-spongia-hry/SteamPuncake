using System;
using UnityEngine;

namespace Levels
{
    public class LevelCompletionZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (LevelController.Instance.PlayingLevel.order == LevelController.CurrentLevel)
                {
                    LevelController.CurrentLevel++;
                    if (LevelController.Instance.PlayingLevel.unlocksNextLocation)
                        LevelController.CurrentLocation++;
                }
                LevelController.Instance.ExitLevel();
            }
        }
    }
}