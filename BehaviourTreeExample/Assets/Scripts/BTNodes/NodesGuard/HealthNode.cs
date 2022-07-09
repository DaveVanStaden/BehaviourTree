using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private Guard enemy;
    private float threshhold;

    public HealthNode(Guard enemy, float threshhold)
    {
        this.enemy = enemy;
        this.threshhold = threshhold;
    }
    public override NodeState Evaluate()
    {
        return enemy.currentHealth <= threshhold ? NodeState.SUCCES : NodeState.FAILURE;
    }
}
