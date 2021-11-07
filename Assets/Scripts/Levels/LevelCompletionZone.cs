using System;
using UnityEngine;

namespace Levels
{
    public class LevelCompletionZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length != 0) return;
            if (LevelController.Instance.PlayingLevel.order == LevelController.CurrentLevel)
            {
                LevelController.CurrentLevel++;
                if (LevelController.Instance.PlayingLevel.unlocksNextLocation)
                    LevelController.CurrentLocation++;
            }

            PlayerMovement.DisableInput = true;
            LevelController.Instance.completeUI.SetActive(true);
        }
    }
}