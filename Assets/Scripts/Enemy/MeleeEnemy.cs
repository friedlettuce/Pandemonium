using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header("Attack Sound")]
    [SerializeField] private AudioClip meleeAttack;

    private void Awake()
    {
        base.Awake();
        attackSound = meleeAttack;
        trigger = "meleeAttack";
    }
}
