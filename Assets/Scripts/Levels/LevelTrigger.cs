using System;
using TMPro;
using UnityEngine;

namespace Levels
{
    public class LevelTrigger : MonoBehaviour
    {
        public Level level;
        private static readonly int Tint = Shader.PropertyToID("Tint");

        private void Start()
        {
            var p = GetComponentInChildren<ParticleSystemRenderer>();
            p.material = new Material(p.material);
            p.material.SetColor(Tint, level.Unlocked ? Color.yellow : Color.gray);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                LevelController.Instance.ShowLevelInfo(level);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                LevelController.Instance.HideLevelInfo();
            }
        }
    }
}