using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage // Will damage player every time
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D coll;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
        hit = false;
        coll.enabled = true;
    }

    private void Update()
    {
        if (hit) return;
        float movement = speed * Time.deltaTime;
        transform.Translate(movement, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision); // parent function
        coll.enabled = false;
        
        if(anim != null)
        {
            anim.SetTrigger("explode"); // fireball explodes
        }
        else
        {
            gameObject.SetActive(false); // deactivate arrow
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
