using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNPC : MonoBehaviour
{
    public Transform target;
    public GameObject gunObject;
    public float attackRange = 8.0f;

    private NavMeshAgent _agent = null;
    private Gun _gun = null;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _gun = gunObject.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (Vector3.SqrMagnitude(_agent.transform.position - target.position) < attackRange * attackRange)
        {
            _agent.isStopped = true;
            _agent.transform.LookAt(target);
            _gun.Shoot();
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(target.position);
        }
    }
}
