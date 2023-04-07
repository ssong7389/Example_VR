using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    private EnemyController enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();    
    }

    public void AimEnter()
    {
        enemy.isLockedOn = true;
        enemy.isHead = true;
    }

    public void AimExit()
    {
        enemy.isLockedOn = false;
        enemy.lockTime = 0f;
        enemy.isHead = false;
    }
}
