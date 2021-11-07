using System;
using TMPro;
using UnityEngine;

namespace Levels
{
    public class LevelTrigger : MonoBehaviour
    {
        public Level level;
        private static readonly int Tint = Shader.PropertyToID("Tint");
        public TextMeshPro text;

        private void Start()
        {
            var p = GetComponentInChildren<ParticleSystemRenderer>();
            p.material = new Material(p.material);
            var color = level.Unlocked ? Color.yellow : Color.gray;
            p.material.SetColor(Tint, color);
            text.text = (level.order + 1).ToString();
            text.color = color;
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