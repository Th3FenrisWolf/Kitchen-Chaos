using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navigation : MonoBehaviour
{
    public Transform target;
    public Transform agentTrans;
    public Transform[] patrolAnchors;
    NavMeshAgent agent;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agentTrans.position, target.position) >= 2 && Vector3.Distance(agentTrans.position, target.position) <= 15)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(patrolAnchors[index].position);
        }
        
        if (pathComplete())
        {
            if (index < patrolAnchors.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
    }

    protected bool pathComplete()
    {
        if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }

        return false;
    }
}
