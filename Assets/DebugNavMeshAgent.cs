using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DebugNavMeshAgent : MonoBehaviour
{
    NavMeshAgent agent;
    public bool desiredVelocity;
    public bool velocity;
    public bool path;
    
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    
    }

    private void OnDrawGizmos() {
        if (velocity){
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,transform.position + agent.velocity);

            
        }

        if(desiredVelocity){
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }

        if (path){
            Gizmos.color = Color.black;
            var agentPath = agent.path;
            Vector3 prevCorner = transform.position;
            foreach (var corner in agentPath.corners){
                Gizmos.DrawLine(prevCorner,corner);
                Gizmos.DrawSphere(corner,0.1f);
                prevCorner = corner;
            }
        }

    }
}
