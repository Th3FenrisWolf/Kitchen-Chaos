using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour
{
    public Transform target;
    public Transform agentTrans;
    public Transform[] patrolAnchors;
    NavMeshAgent agent;
    private int index = 0;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    IEnumerator ReloadScene() {
        yield return new WaitForSeconds(2);

        // reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        // player dead
        if (Vector3.Distance(agentTrans.position, target.position) < 2) {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.ResetPath();

            // TODO play eat animation and show death/respawn option on screen
            anim.SetTrigger("eat");

            // lock the player movement
            StartCoroutine(ReloadScene());
        }


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
