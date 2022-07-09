using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInFieldOfViewPatrolNode : Node
{
    private FieldOfView fov;
    private Guard ai;

    public IsInFieldOfViewPatrolNode(FieldOfView fov, Guard ai)
    {
        this.fov = fov;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (fov.PlayerVisible)
        {
            if (!ai.isGettingWeapon && !ai.hasWeapon && !ai.isAttacking)
            {
                ai.isGettingWeapon = true;
            }
            return NodeState.FAILURE;
        }
        else
        {
            if (!ai.isGettingWeapon)
                return NodeState.SUCCES;
            else
                return NodeState.FAILURE;

        }
    }
}
