using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GetWeaponNode : Node
{
    private Transform weaponLocation;
    private NavMeshAgent agent;
    private Guard ai;
    private float maxTime = 4f;
    private float time;

    public GetWeaponNode(Transform weaponLocation, NavMeshAgent agent, Guard ai)
    {
        this.weaponLocation = weaponLocation;
        this.agent = agent;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(agent.transform.position, weaponLocation.position);
        if (distance > 0.2f)
        {
            ai.SetColor(Color.blue);
            agent.isStopped = false;
            agent.SetDestination(weaponLocation.position);
            return NodeState.RUNNING;
        }
        else
        {
            time += Time.deltaTime;
            if (time >= maxTime)
            {
                maxTime = 0f;
                ai.hasWeapon = true;
                ai.isGettingWeapon = false;
                return NodeState.SUCCES;
            }

            return NodeState.RUNNING;
        }
    }
}
