using System;
using DamageSystem;
using UnityEngine;

public class HealthRegenZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Heal();
            Destroy(gameObject);
        }
    }
}