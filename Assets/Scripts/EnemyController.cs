using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rb;
    float timer;
    int direction = 1;

    Animator animator;

    bool broken = true;

    public ParticleSystem smokeEffect;

    public AudioClip collectedClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!broken)
        {
            return;
        }   //if

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }   //if

        Vector2 position = rb.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * enemySpeed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }   //if
        else
        {
            position.x = position.x + Time.deltaTime * enemySpeed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }   //else

        rb.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController rc = other.gameObject.GetComponent<RubyController>();

        if(rc != null)
        {
            rc.changeHealth(-1);

            rc.PlaySound(collectedClip);
        }   //if
    }

    public void Fix()
    {
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        broken = false;
        rb.simulated = false;
    }
}
