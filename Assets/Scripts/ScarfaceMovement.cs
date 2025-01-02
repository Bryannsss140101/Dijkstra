using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ScarfaceMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    public bool Stopped
    {
        get => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    public void HandleMovement(Transform target)
    {
        if (!target)
            return;

        agent.SetDestination(target.position);
    }

    public Vector3 Velocity()
    {
        var direction = (agent.destination - transform.position).normalized;
        var angle = Vector3.SignedAngle(transform.forward, direction, transform.up);

        var y = Mathf.InverseLerp(-180f, 180f, angle) * 2f - 1f;
        var z = Mathf.InverseLerp(0f, agent.speed, agent.velocity.magnitude);

        return new Vector3(0f, y, z);
    }
}