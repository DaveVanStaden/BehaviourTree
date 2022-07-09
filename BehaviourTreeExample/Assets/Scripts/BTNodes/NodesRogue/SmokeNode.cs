using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeNode : Node
{
    private Rogue ai;
    private Transform target;

    public SmokeNode(Rogue ai, Transform target)
    {
        this.ai = ai;
        this.target = target;
    }

    public override NodeState Evaluate()
    {
        ai.SetColor(Color.red);
        ai.transform.rotation = Quaternion.RotateTowards(ai.transform.rotation,target.rotation,10 * Time.deltaTime);
        ai.InstantiateSmokeBomb();
        return NodeState.SUCCES;
    }
}
