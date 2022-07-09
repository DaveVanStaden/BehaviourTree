using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttackNode : Node
{
    private Guard ai;

    public CanAttackNode(Guard ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (ai.hasWeapon)
        {
            return NodeState.SUCCES;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
