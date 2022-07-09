using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private Guard ai;

    public AttackNode(NavMeshAgent agent, Guard ai, Transform target)
    {
        this.agent = agent;
        this.ai = ai;
        this.target = target;
    }

    public override NodeState Evaluate()
    {
        if (ai.canAttack)
        {
            agent.isStopped = true;
            ai.isAttacking = true;
            ai.transform.rotation = Quaternion.RotateTowards(ai.transform.rotation,target.rotation,10 * Time.deltaTime);
            //Vector3 RotateTowards = Vector3.RotateTowards(ai.gameObject.transform.forward, agent.destination,
                //1.0f * Time.deltaTime, 0.0f);
            //agent.transform.rotation = Quaternion.LookRotation(RotateTowards);
            ai.SetColor(Color.green);
            return NodeState.RUNNING;
        }
        else
        {
            ai.isAttacking = false;
            return NodeState.FAILURE;
        }
            

    }
}
