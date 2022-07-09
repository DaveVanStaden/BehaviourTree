using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasWeaponNode : Node
{
    private Guard enemy;
    private Rogue ai;

    public HasWeaponNode(Guard enemy, Rogue ai)
    {
        this.enemy = enemy;
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        if (enemy.hasWeapon && enemy.fov.PlayerVisible)
        {
            ai.smokeEnemy = true;
            return NodeState.SUCCES;
        }
        else
        {
            ai.smokeEnemy = false;
            return NodeState.FAILURE;
        }
    }
}
