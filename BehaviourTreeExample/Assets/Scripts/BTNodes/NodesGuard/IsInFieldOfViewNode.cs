using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInFieldOfViewNode : Node
{
    private FieldOfView fov;
    private Guard ai;


    public IsInFieldOfViewNode(FieldOfView fov, Guard ai)
    {
        this.fov = fov;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (ai.isGettingWeapon && !ai.hasWeapon && !ai.isAttacking)
        {
            return NodeState.SUCCES;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
