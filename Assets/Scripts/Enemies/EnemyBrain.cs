using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Transform target;

    private EnemyReferences _enemyReferences;
    private float _attackingDistance;

    private float _pathUpdateDeadLine;

    private void Awake()
    {
        _enemyReferences = GetComponent<EnemyReferences>();
    }

    private void Start()
    {
        _attackingDistance = _enemyReferences.navMeshAgent.stoppingDistance;
    }

    private void Update()
    {
        if (target is not null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= _attackingDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
        
            _enemyReferences.animator.SetBool("attacking", inRange);   
        }
        
        _enemyReferences.animator.SetFloat("speed", _enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }
    
    private void UpdatePath()
    {
        bool isAttacking = _enemyReferences.animator.GetBool("attacking");

        if (isAttacking) return;
        
        if (Time.time >= _pathUpdateDeadLine)
        {
            _pathUpdateDeadLine = Time.time + _enemyReferences.pathUpdateDelay;
            _enemyReferences.navMeshAgent.SetDestination(target.position);   
        }
    }
}
