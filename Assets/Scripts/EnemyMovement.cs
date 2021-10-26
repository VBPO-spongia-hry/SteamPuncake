using System;
using DamageSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, ITickable
{
    public Transform player;
    private NavMeshAgent _agent;
    private Animator _animator;
    private EnemyHealth _health;
    private static readonly int Speed = Animator.StringToHash("speed");

    public void OnSpawn()
    {
        _health = GetComponent<EnemyHealth>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void OnGameTick()
    {
        if (_health.IsInCombatMode || _health.IsDead) return;
        _agent.SetDestination(player.position);
    }

    private void Update()
    {
        _animator.transform.localPosition = Vector3.zero;
        _animator.transform.localRotation = Quaternion.identity;
        _animator.SetFloat(Speed, _agent.velocity.magnitude);
    }
}