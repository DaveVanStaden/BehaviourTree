using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerNode : Node
{
    private Rogue ai;
    private Transform player;
    private NavMeshAgent agent;
    private float followDistance;

    public FollowPlayerNode(Rogue ai, Transform player, NavMeshAgent agent, float followDistance)
    {
        this.ai = ai;
        this.player = player;
        this.agent = agent;
        this.followDistance = followDistance;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(ai.gameObject.transform.position, player.transform.position);
        if (distance >= followDistance && !ai.smokeEnemy)
        {
            ai.SetColor(Color.magenta);
            agent.SetDestination(player.transform.position);
            return NodeState.RUNNING;
        }
        else
        {
            agent.SetDestination(ai.transform.position);
            return NodeState.SUCCES;
        }
    }
}
