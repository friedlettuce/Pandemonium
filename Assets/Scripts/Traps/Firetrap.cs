using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [Header("SFX")]
    [SerializeField] private AudioClip fireSound;

    private Health playerHealth;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            playerHealth = collision.GetComponent<Health>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        // triggering trap
        triggered = true;
        spriteRend.color = Color.red;
        
        // activates the trap
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", active);
        SoundManager.instance.PlaySound(fireSound);

        // turns off trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", active);
    }
}
