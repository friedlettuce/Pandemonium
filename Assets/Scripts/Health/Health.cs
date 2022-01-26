using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("IFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int flashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        dead = false;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            // iframes
            StartCoroutine(Invulnerability());
        }
        else if(!dead)
        {
            anim.SetTrigger("die");
            GetComponent<PlayerMovement>().enabled = false;
            dead = true;
        }
    }

    public void AddHealth(float _val)
    {
        currentHealth = Mathf.Clamp(currentHealth + _val, 0, startingHealth);
    }
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < flashes; ++i)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (flashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (flashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
