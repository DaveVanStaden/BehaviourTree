using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeEnemyNode : Node
{
    private Rogue ai;
    private FieldOfView fov;

    public SeeEnemyNode(Rogue ai, FieldOfView fov)
    {
        this.ai = ai;
        this.fov = fov;
    }

    public override NodeState Evaluate()
    {
        if (fov.PlayerVisible)
        {
            return NodeState.SUCCES;
        }
        else
        {
            ai.smokeEnemy = false;
            return NodeState.FAILURE;
        }
    }
}
