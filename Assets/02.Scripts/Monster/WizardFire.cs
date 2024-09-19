using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardFire : Monster
{
    public override void Init()
    {
        ed.state = EnemyState.Idle;

        ed.maxhp = 50;
        ed.hp = ed.maxhp;
        ed.damage = 5;

        ed.atkRange = 2.5f;
        ed.atkDelay = 2;
    }
}
