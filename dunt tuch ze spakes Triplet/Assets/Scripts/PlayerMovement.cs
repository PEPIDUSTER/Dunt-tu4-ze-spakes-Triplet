using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private Animator anim;
    private GameController SC;

    [Header("Movement")]
    public float moveSpeed = 3.0f;
    public float jumpForce = 4.0f;

    private bool idle = true;
    private bool isDead = false;

    private bool mflag = false;
    private Vector2 pos1, pos2;
    private float minDst=0.01f; 
    private float progress;
    private float AnimationSpeed = 1.5f;

    [Header("Walls")]
    public SpikeSpawner wallR;
    public SpikeSpawner wallL;

    void Start()
    {
        pos1 = transform.position;    
        pos2 = new Vector2 (0.0f, 1.5f);  

        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SC = GameObject.Find("Main Camera").GetComponent<GameController>();
    }

    private void FixedUpdate() {
        if (idle) {
            rigidBody.gravityScale = 0;
            idleAnimation();
        } else if (isDead) {
            rigidBody.gravityScale = 1;
        } else {
            rigidBody.gravityScale = 1;
            rigidBody.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidBody.velocity.y);
        }
    }

    void Update()
    {
        if (!isDead) {
            if(Input.GetButtonDown("Jump")) {
                jump();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "spike") {
            isDead = true;
            anim.SetBool("isDead",true);
            Destroy(gameObject, 5);
        }
    }
    private void OnTriggerEnter2D(Collider2D col) {
        if (!isDead) {
            if (col.gameObject.name == "wall left") {
                flip();
                SC.score += 1;
                moveSpeed += 0.04f;
                wallR.CreateLine();
                wallL.DestroyLine();

            } else if (col.gameObject.name == "wall right") {
                flip();
                SC.score += 1;
                moveSpeed += 0.04f;
                wallL.CreateLine();
                wallR.DestroyLine();
            }
        }
    }

    void jump() {
        rigidBody.velocity = new Vector2(transform.localScale.x, jumpForce);
        idle = false;
    }

    void flip() {
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void idleAnimation () {
        if (mflag) {
           
            transform.position = Vector2.Lerp(pos1, pos2, progress);
            progress += Time.deltaTime*AnimationSpeed;   
            if (Vector2.Distance(transform.position,pos2)<minDst) {
              mflag=!mflag;
            }

        } else {
            transform.position = Vector2.Lerp(pos1, pos2, progress);
            progress -= Time.deltaTime*AnimationSpeed;
            if (Vector2.Distance(transform.position,pos1)<minDst) {
              mflag=!mflag;
            }
        }
    }
}
