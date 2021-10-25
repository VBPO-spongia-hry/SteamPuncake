using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _agent.SetDestination(player.position);
        _animator.transform.localPosition = Vector3.zero;
        _animator.transform.localRotation = Quaternion.identity;
        _animator.SetFloat("speed", _agent.velocity.magnitude);
    }
}