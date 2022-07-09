using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float range;

    private Transform target;

    private Transform origin;

    private Guard ai;
    

    public RangeNode(float range, Transform target, Transform origin, Guard ai)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, origin.position);
        if (distance < range)
        {
            return NodeState.SUCCES;
        }
        else
        {
            if (ai.isAttacking)
                ai.isAttacking = false;
            return NodeState.FAILURE;
        }
            
    }
}
