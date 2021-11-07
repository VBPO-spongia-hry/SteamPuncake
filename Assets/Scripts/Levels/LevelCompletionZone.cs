using System;
using System.Linq;
using UnityEngine;

namespace Levels
{
    public class LevelCompletionZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (!CanComplete()) return;
            if (LevelController.Instance.PlayingLevel.order == LevelController.CurrentLevel)
            {
                LevelController.CurrentLevel++;
                if (LevelController.Instance.PlayingLevel.unlocksNextLocation)
                    LevelController.CurrentLocation++;
            }

            PlayerMovement.DisableInput = true;
            LevelController.Instance.completeUI.SetActive(true);
            GameController.Instance.ResetCombo();
        }

        private static bool CanComplete()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > 0)
                return false;
            var spawnerTriggers = FindObjectsOfType<SpawnerTrigger>();
            return spawnerTriggers.Where(trigger => !trigger.optional).All(trigger => trigger.Activated);
        }
    }
}