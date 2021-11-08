using System;
using DamageSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour, ITickable
{
    public Transform player;
    [Tooltip("Should wait until previous enemy is defeated?")]
    public bool wait = true;
    public float stopDistance = 1.5f;
    private NavMeshAgent _agent;
    private Animator _animator;
    private EnemyHealth _health;
    private CapsuleCollider _collider;
    private static readonly int Speed = Animator.StringToHash("speed");

    public void OnSpawn()
    {
        _health = GetComponent<EnemyHealth>();
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<CapsuleCollider>();
    }

    public void OnGameTick()
    {
        if (_health.IsInCombatMode || _health.IsDead || PlayerMovement.DisableInput) return;
        SetDestination(player.position);
    }

    public void SetDestination(Vector3 pos)
    {
        _agent.SetDestination(pos);
    }
    
    private void Update()
    {
        _agent.enabled = !PlayerMovement.DisableInput;
        _animator.transform.localPosition = Vector3.zero;
        _animator.transform.localRotation = Quaternion.identity;
        if(wait)
            _agent.isStopped = IsBlocked();
        Vector3 dir = player.position - transform.position;
        dir.y = 0;//This allows the object to only rotate on its y axis
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, 1);    
        _animator.SetFloat(Speed, _agent.velocity.magnitude);
    }

    private bool IsBlocked()
    {
        var start = transform.position + new Vector3(0, _collider.height / 2f) + _collider.center;
        var end = transform.position - new Vector3(0, _collider.height / 2f) + _collider.center;
        if (Physics.CapsuleCast(start, end, 1, transform.forward, out var hit, stopDistance))
        {
            if (hit.collider.GetComponent<EnemyMovement>())
            {
                return true;
            }
        }

        return false;
    }
}