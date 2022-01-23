using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float normalGravity;

    private float horizontal_input;

    private float wallJumpCooldown; // Also sets time until can move in air after jump off wall
    private readonly float wallJumpUp = 6;
    private readonly float wallJumpSide = 3;

    private void Awake()
    {
        // Finds references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        normalGravity = GetComponent<Rigidbody2D>().gravityScale;
    }

    private void Update()
    {
        horizontal_input = Input.GetAxis("Horizontal");

        if(horizontal_input > .01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontal_input < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // flips img
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
        }

        // Set animators by if player moving
        anim.SetBool("run", horizontal_input != 0);
        anim.SetBool("grounded", isGrounded());

        // Wall jump logic
        if(wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontal_input * speed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = normalGravity;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontal_input == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpSide * 4, -wallJumpUp * 1.5f);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpSide, wallJumpUp);
            }
            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontal_input == 0 && isGrounded() & !onWall();
    }
}
