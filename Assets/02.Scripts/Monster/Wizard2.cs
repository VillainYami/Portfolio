using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard2 : Monster
{
    public override void Init()
    {
        ed.state = EnemyState.Idle;

        ed.maxhp = 50;
        ed.hp = ed.maxhp;
        ed.damage = 10;

        ed.atkReady = 1;
        ed.atkRange = 2.5f;
        ed.atkDelay = 2;
    }

}
