using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNode : Node
{
    private Guard ai;
    private NavMeshAgent agent;
    private Transform[] location;
    private int pointIndex;

    public PatrolNode(NavMeshAgent agent, Transform[] location, Guard ai)
    {
        this.agent = agent;
        this.location = location;
        this.ai = ai;
        //this.pointIndex = pointIndex;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(location[pointIndex].transform.position, agent.transform.position);
        if (distance > 0.2f)
        {
            ai.SetColor(Color.magenta);
            agent.isStopped = false;
            agent.SetDestination(location[pointIndex].transform.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.isStopped = true;
            pointIndex += 1;
            if (pointIndex >= location.Length)
            {
                pointIndex = 0;
            }
            return NodeState.SUCCES;
        }
    }
}
