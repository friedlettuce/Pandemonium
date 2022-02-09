using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    [Header("SFX")]
    [SerializeField] private AudioClip shootSound;

    private float cooldownTimer;
    private void Attack()
    {
        cooldownTimer = 0;

        arrows[findArrow()].transform.position = firePoint.position;
        arrows[findArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
        SoundManager.instance.PlaySound(shootSound);
    }

    private int findArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
