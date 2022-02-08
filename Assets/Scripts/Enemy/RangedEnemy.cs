using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Fireball Sound")]
    [SerializeField] private AudioClip fireballSound;

    private void Awake()
    {
        base.Awake();
        trigger = "rangedAttack";
        checkAlive = false;
        attackSound = fireballSound;
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy) return i;
        }
        return 0;
    }
}
