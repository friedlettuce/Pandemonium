using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private PlayerMovement plyr_mvmnt;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        plyr_mvmnt = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && cooldownTimer > attackCooldown && plyr_mvmnt.canAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //if(SoundManager.instance != null)
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // object pooling saves used objects to reuse, deactivate instead of destroy
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for(int fireball = 0; fireball < fireballs.Length; ++fireball)
        {
            if (!fireballs[fireball].activeInHierarchy)
            {
                return fireball;
            }
        }
        return 0;
    }
}
