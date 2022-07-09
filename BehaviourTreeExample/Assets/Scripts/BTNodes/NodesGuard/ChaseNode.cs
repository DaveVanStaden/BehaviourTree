using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private Guard ai;
    private bool hasWeapon;
    public ChaseNode(Transform target, NavMeshAgent agent, Guard ai, bool hasWeapon)
    {
        this.target = target;
        this.agent = agent;
        this.ai = ai;
        this.hasWeapon = hasWeapon;
    }

    public override NodeState Evaluate()
    {
        hasWeapon = ai.hasWeapon;
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if (hasWeapon && !ai.isAttacking)
        {
            if (distance > 5f)
            {
                ai.SetColor(Color.cyan);
                agent.isStopped = false;
                agent.SetDestination(target.position);
                return NodeState.RUNNING;
            }
            else
            {
                ai.canAttack = true;
                agent.isStopped = true;
                return NodeState.SUCCES;
            }
        }
        else
        {
            return NodeState.FAILURE;
        }

    }
}
